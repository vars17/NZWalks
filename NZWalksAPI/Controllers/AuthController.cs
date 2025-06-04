using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        //inject a usermanager class
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        
        // POST :/api/Auth/Register
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerreqdto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerreqdto.Username,
                Email=registerreqdto.Username

            };

            var identityResult=await userManager.CreateAsync(identityUser,registerreqdto.Password);
            if (identityResult.Succeeded)
            {
                //Add roles to this User
                if (registerreqdto.Roles != null && registerreqdto.Roles.Any())
                {

                    identityResult=await userManager.AddToRolesAsync(identityUser, registerreqdto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login");
                    }
                }

            }
            return Ok("Something went wrong");
        }

        //POST :/api/Auth/Login
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto logreqdto)
        {
            var user = await userManager.FindByEmailAsync(logreqdto.Username);

            if(user!=null)
            {
                var checkPassResult=await userManager.CheckPasswordAsync(user, logreqdto.Password);
                if(checkPassResult)
                {
                    //create token
                    return Ok();

                }

            }
            return BadRequest("Username or Password is incorrect!");
        }
    }
}
