using ProjectManagementApp.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class EmailService
    {
        private readonly EmailConfiguration emailConfiguration;
        private readonly SendGridClient client;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration;
            client = new SendGridClient(emailConfiguration.ApiKey);
        }

        public async Task<bool> SendTestEmail(string message)
        {
            var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailConfiguration.FromAddress, "Dragos"),
                new EmailAddress("moiseanurares@gmail.com", "Rares"),
                "Test Subject",
                message,
                null);

            var response = await client.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> SendEmailConfirmation(string email, string code)
        {
            var message = MailHelper.CreateSingleTemplateEmail(
                new EmailAddress(emailConfiguration.FromAddress, "TrelloV2"),
                new EmailAddress(email, "Client"),
                emailConfiguration.EmailConfigurationTemplateId,
                new Dictionary<string, string>
                {
                    {"confirmation-code", code},
                    {"sent-date", DateTime.Now.ToString() }
                });

            string test;
            try
            {
                var response = await client.SendEmailAsync(message);
                if (response.IsSuccessStatusCode)
                    return true;
            } catch (Exception ex)
            {
                test = ex.ToString();
            }

            return false;
        }
    }
}
