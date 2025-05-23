using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        //GEThttps://localhost:7163/api/Student

        public IActionResult GetAllStudent()
        {
            string[] students = new string[] { "Celaena", "Ananya", "Rhysand", "William", "Emma" };
            return Ok(students);
        }
    }
}
