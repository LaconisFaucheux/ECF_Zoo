namespace API_Arcadia.Models.Data
{
    public class UserWithRolesDTO
    {
        public string id { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public List<string> roles { get; set; }
    }
}
