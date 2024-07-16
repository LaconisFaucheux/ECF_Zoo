using Microsoft.AspNetCore.Identity;

namespace API_Arcadia.Interfaces
{
    public interface ITokenService
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
