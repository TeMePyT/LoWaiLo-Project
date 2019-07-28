namespace LoWaiLo.Services.Messaging
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Options;

    public class MessageSender : IEmailSender
    {
        private readonly SendGridEmailSender emailSender;
        private readonly EmailSettings emailSettings;

        public MessageSender(IOptions<EmailSettings> settings)
        {
            this.emailSettings = settings.Value;
            this.emailSender = new SendGridEmailSender(this.emailSettings.ApiKey, this.emailSettings.FromEmail, this.emailSettings.FromName);
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return this.emailSender.SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
