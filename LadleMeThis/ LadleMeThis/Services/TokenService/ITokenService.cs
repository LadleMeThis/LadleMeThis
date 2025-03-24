using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Services.TokenService
{
    public interface ITokenService
    {
        public Task<string> CreateToken(IdentityUser user);
    }
}
