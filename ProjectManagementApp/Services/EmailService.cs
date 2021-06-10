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
