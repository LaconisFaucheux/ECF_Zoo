using API_Arcadia.Interfaces;
using API_Arcadia.Models.Data;
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
        public IActionResult SendEmailAsAdmin(EmailDTO mail)
        {
            if (_emailSender.SendEmailAsAdminAsync(mail.body, mail.to, mail.subject))
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
        public IActionResult SendEmailAsEmployee(EmailDTO mail)
        {
            if (_emailSender.SendEmailAsEmployeeAsync(mail.body, mail.to, mail.subject))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("mail-as-visitor")]
        public IActionResult SendEmailAsVisitor(EmailDTO mail)
        {
            if (_emailSender.SendEmailAsVisitorAsync(mail.body, mail.subject))
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
