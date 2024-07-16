using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace API_Arcadia.Models.Data
{
    public class LoginRequestDTO
    {
        public string? Email {  get; set; }
        public string? Password { get; set; }
    }
}
