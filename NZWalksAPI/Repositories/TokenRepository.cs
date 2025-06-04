using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            
        }
    }
}
