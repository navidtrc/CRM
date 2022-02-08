using EmailSender.Models;
using EmailSender.Service;
using System.Threading.Tasks;

namespace CRM.Application.Services.Email
{
    public class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        private readonly IEmailSender emailSender;
        public EmailSender(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new Message(new string[] { email }, subject, htmlMessage, null);
            await emailSender.SendEmailAsync(message);
        }
    }
}
