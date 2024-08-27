namespace API_Arcadia.Models.Data
{
    public class LoginResponseDTO
    {
        public string? userId { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public List<string>? Roles { get; set; }
    }
}
