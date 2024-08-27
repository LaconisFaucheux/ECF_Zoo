using System.ComponentModel.DataAnnotations;

namespace API_Arcadia.Models.Data
{
    public class UpdatePasswordDTO
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty ;
        public string OldPassword { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }
}
