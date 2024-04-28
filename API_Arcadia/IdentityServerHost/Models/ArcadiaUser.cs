using Microsoft.AspNetCore.Identity;

namespace IdentityServerHost.Models
{
    public class ArcadiaUser : IdentityUser<int>
    {
        public string Fonction {  get; set; } = string.Empty;
    }
}
