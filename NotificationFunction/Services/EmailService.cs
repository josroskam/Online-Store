using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using NotificationFunction.Models;

namespace NotificationFunction.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailClient _emailClient;
        private const string SenderAddress = "DoNotReply@a67f001a-b31b-4146-b5bc-4c5cb7907aa5.azurecomm.net"; 

        public EmailService(string connectionString)
        {
            _emailClient = new EmailClient(connectionString);
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string message)
        {
            // Create email content
            var emailContent = new EmailContent(subject)
            {
                PlainText = message,
                Html = $"<html><body><p>{message}</p></body></html>"
            };

            // Add recipient email address
            var emailRecipients = new EmailRecipients(new List<EmailAddress> { new EmailAddress(recipientEmail) });

            // Create the email message with sender address, recipients, and content
            var emailMessage = new EmailMessage(SenderAddress, emailRecipients, emailContent);

            try
            {
                // Send the email and wait until completion
                EmailSendOperation emailSendOperation = await _emailClient.SendAsync(WaitUntil.Completed, emailMessage);
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
    }
}
