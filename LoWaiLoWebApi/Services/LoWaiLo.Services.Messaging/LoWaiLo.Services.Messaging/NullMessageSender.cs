namespace LoWaiLo.Services.Messaging
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using System;
    using System.Threading.Tasks;

    public class NullMessageSender : IEmailSender
    {
        private const string ApiKey = "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";
        private const string fromEmail = "noreply@LoWaiLo.com";
        private const string fromName = "LoWaiLo";
        private  SendGridEmailSender emailSender;

        public NullMessageSender()
        {
            this.emailSender = new SendGridEmailSender(ApiKey, fromEmail, fromName);
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return this.emailSender.SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
