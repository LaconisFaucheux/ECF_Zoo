using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace API_Arcadia.Services
{
    public class EmailService : IEmailSender
    {
        private readonly EmailServiceConfig? _emailServiceConfig;
        public EmailService(IConfiguration configuration)
        {
            _emailServiceConfig = configuration.GetSection("EmailConfig").Get<EmailServiceConfig>();
        }
        public bool SendEmailAsAdminAsync(string body, string to, string subject)
        {
            if (_emailServiceConfig == null) return false;

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailServiceConfig.AsAdmin!.id));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using var smtp = new SmtpClient();
                smtp.Connect(_emailServiceConfig.config!.smtp, _emailServiceConfig.config!.port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailServiceConfig.AsAdmin.id, _emailServiceConfig.AsAdmin.pwd);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendEmailAsEmployeeAsync(string body, string to, string subject)
        {
            if (_emailServiceConfig == null) return false;

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailServiceConfig.AsEmployee!.id));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using var smtp = new SmtpClient();
                smtp.Connect(_emailServiceConfig.config!.smtp, _emailServiceConfig.config!.port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailServiceConfig.AsEmployee.id, _emailServiceConfig.AsEmployee.pwd);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendEmailAsVisitorAsync(string body, string subject)
        {
            if (_emailServiceConfig == null) return false;

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailServiceConfig.AsVisitor!.id));
                email.To.Add(MailboxAddress.Parse(_emailServiceConfig.AsEmployee!.id));//Visitor can ONLY send mail to contact
                email.Subject = subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using var smtp = new SmtpClient();
                smtp.Connect(_emailServiceConfig.config!.smtp, _emailServiceConfig.config!.port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailServiceConfig.AsVisitor.id, _emailServiceConfig.AsVisitor.pwd);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
