using API_Arcadia.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
                _emailSender = emailSender;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("mail-as-admin")]
        public IActionResult SendEmailAsAdmin(string body, string to, string subject)
        {
            if (_emailSender.SendEmailAsAdminAsync(body, to, subject))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }            
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("mail-as-employee")]
        public IActionResult SendEmailAsEmployee(string body, string to, string subject)
        {
            if (_emailSender.SendEmailAsEmployeeAsync(body, to, subject))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("mail-as-visitor")]
        public IActionResult SendEmailAsVisitor(string body, string subject)
        {
            if (_emailSender.SendEmailAsVisitorAsync(body, subject))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
