using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace API_Arcadia.Models.Data
{
    public class LoginRequestDTO
    {
        [Required]
        public string? Email {  get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
