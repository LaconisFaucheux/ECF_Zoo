namespace API_Arcadia.Models
{
    public class AuthDbContextConfig
    {
        public string AdminRoleId { get; set; } = string.Empty;
        public string EmployeeRoleId { get; set; } = string.Empty;
        public string VetRoleId { get; set; } = string.Empty;
        public string AdminUserId { get; set; } = string.Empty;
        public string AdminInitialPassword { get; set; } = string.Empty;
    }
}
