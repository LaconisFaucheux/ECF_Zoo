using System.Security.Policy;

namespace API_Arcadia.Models.Data
{
    public class EmailDTO
    {
        public string? to { get; set; } = string.Empty;
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
    }
}
