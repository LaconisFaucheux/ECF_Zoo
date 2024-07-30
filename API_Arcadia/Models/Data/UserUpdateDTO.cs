using System.ComponentModel.DataAnnotations;

namespace API_Arcadia.Models.Data
{
    public class UserUpdateDTO
    {
        [Required]
        public string id { get; set; } = string.Empty;
        [Required]
        public string email { get; set; } = string.Empty;
        [Required]
        public List<string> roles { get; set; }
    }
}
