using Inficare.Application.Common.Interfaces;
using Inficare.Application.Common.Models;
using Inficare.Infrastructure.Common.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Inficare.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly EmailOptions _emailConfiguration;

        public EmailService(ISendGridClient sendGridClient,
                            IOptions<EmailOptions> emailConfiguration)
        {
            _sendGridClient = sendGridClient;
            _emailConfiguration = emailConfiguration.Value;
        }

        public async Task SendAsync(EmailModel email, CancellationToken cancellationToken)
        {
            var hostEmail = string.IsNullOrEmpty(email.From) ? _emailConfiguration.HostEmail : email.From;

            EmailAddress from = new EmailAddress(hostEmail);
            EmailAddress recipient = new EmailAddress(email.To);
            string subject = string.IsNullOrEmpty(email.Subject) ? "inficare" : email.Subject;

            var sendGridMessage = new SendGridMessage()
            {
                From = from,
                Subject = subject,
                HtmlContent = email.Body,
                Personalizations = new List<Personalization>
                {
                    new Personalization
                    {
                        Ccs = email.Ccs?.Select(s => new EmailAddress(s)).ToList()
                    }
                }
            };

            sendGridMessage.AddTo(recipient);

            Response response = await _sendGridClient.SendEmailAsync(sendGridMessage, cancellationToken).ConfigureAwait(false);

            if (response != null && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine($"Email not sent. Status >>> {response.StatusCode}");
            }
        }
    }
}
