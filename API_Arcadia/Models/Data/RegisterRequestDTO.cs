using System.ComponentModel.DataAnnotations;

namespace API_Arcadia.Models.Data
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        [Required]
        public string Role { get; set; } = String.Empty ;
    }
}
