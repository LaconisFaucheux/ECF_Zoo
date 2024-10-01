using Microsoft.AspNetCore.Mvc;

namespace API_Arcadia.Interfaces
{
    public interface IEmailSender
    {
        public bool SendEmailAsAdminAsync(string body, string to, string subject);
        public bool SendEmailAsEmployeeAsync(string body, string to, string subject);
        public bool SendEmailAsVisitorAsync(string body, string subject);
    }
}
