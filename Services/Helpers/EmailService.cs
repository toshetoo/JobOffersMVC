using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.Settings;
using JobOffersMVC.ViewModels.Emails;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Helpers
{
    public class EmailService : IEmailService
    {
        private EmailSettings settings;

        public EmailService(ApplicationSettings applicationSettings)
        {
            this.settings = applicationSettings.EmailSettings;
        }

        public async Task SendEmailAsync(EmailConfig config)
        {
            MailboxAddress from = new MailboxAddress(settings.EmailName, settings.EmailAccount);
            MailboxAddress to = new MailboxAddress(config.ReceiverName, config.ReceiverEmail);

            BodyBuilder builder = new BodyBuilder();
            builder.TextBody = config.EmailBody;

            MimeMessage email = new MimeMessage
            {
                Subject = config.EmailSubject,
                Body = builder.ToMessageBody()
            };
            email.From.Add(from);
            email.To.Add(to);

            SmtpClient client = new SmtpClient();
            client.Connect(settings.Host, settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(settings.EmailAccount, settings.EmailPassword);

            await client.SendAsync(email);

            client.Disconnect(true);
            client.Dispose();
        }
    }
}
