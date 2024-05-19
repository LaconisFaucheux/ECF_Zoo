using System.Security.Claims;

namespace IdentityServerHost.Models.Data
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<ClaimDTO> Claims { get; set; } = new List<ClaimDTO>();
    }
}
