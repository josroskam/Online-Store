using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NotificationFunction.Models;
using NotificationFunction.Services;

namespace NotificationFunction
{
    public class NotificationFunction
    {
        private readonly IEmailService _emailService;

        public NotificationFunction(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [Function("SendNotification")]
        public async Task Run([QueueTrigger("notifications")] string base64Message, FunctionContext context)
        {
            var logger = context.GetLogger("NotificationFunction");
            logger.LogInformation($"Processing notification message: {base64Message}");

            // Decode the base64-encoded JSON string
            var decodedMessage = Encoding.UTF8.GetString(Convert.FromBase64String(base64Message));

            // Deserialize the decoded JSON string to get recipient details
            var notificationDetails = JsonConvert.DeserializeObject<NotificationMessage>(decodedMessage);

            // Send notification email using the injected EmailService
            await _emailService.SendEmailAsync(notificationDetails.RecipientEmail, notificationDetails.Subject, notificationDetails.Message);

            logger.LogInformation("Notification email sent.");
        }
    }
}
