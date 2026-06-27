using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionPatternDatabaseExample.Application.Interfaces;
using OptionPatternDatabaseExample.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OptionPatternDatabaseExample.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<EmailOptions> emailOptions,
            ILogger<EmailService> logger)
        {
            _emailOptions = emailOptions.Value;
            _logger = logger;
        }

        public async Task SendNotificationAsync(string email, string message)
        {
            try
            {
                using var client = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.Port)
                {
                    Credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailOptions.FromEmail),
                    Subject = "User Processing Notification",
                    Body = message,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent to {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {email}");
                throw;
            }
        }
    }
}
