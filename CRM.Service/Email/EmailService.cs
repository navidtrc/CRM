using CRM.Entities.HelperModels;
using CRM.Entities.DataModels.Security;
using CRM.Repository.Core;
using CRM.ViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork unitOfWork;
        public EmailService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> ComposeAsync(ComposeEmailViewModel emailSetting, CancellationToken cancellationToken)
        {
            //string body = string.Format(unitOfWork.ContactSettings.TableNoTracking.SingleOrDefault().ServerToContact, emailSetting.Message);
            await SendAsync(emailSetting.SendTo, emailSetting.Subject, "body", cancellationToken);
            return true;
        }
        public async Task<bool> SendAsync(string sendTo, string subject, string body, CancellationToken cancellationToken)
        {
            try
            {
                var emailSetting = await unitOfWork.Settings.TableNoTracking.FirstOrDefaultAsync(f => f.Key == EmailConfig.Key, cancellationToken);
                var setting = JsonConvert.DeserializeObject<EmailConfig>(Encoding.UTF8.GetString(emailSetting.Content));

                var message = new MailMessage();
                message.To.Add(new MailAddress(sendTo));
                message.From = new MailAddress(setting.UserName);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = setting.UserName,
                        Password = setting.Password
                    };
                    smtp.Credentials = credential;
                    smtp.Host = setting.SmtpServer;
                    smtp.Port = setting.SmtpPort;
                    smtp.EnableSsl = setting.EnableSSL;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                //throw new AppException("Email Sending Error", ex.InnerException);
            }

        }
        public async Task<bool> UpdateAsync(EmailConfig emailConfig, CancellationToken cancellationToken)
        {
            var email = await unitOfWork.Settings.TableNoTracking.FirstOrDefaultAsync(f => f.Key == EmailConfig.Key, cancellationToken);
            if (email != null)
                await unitOfWork.Settings.DeleteAsync(email, cancellationToken);
            Setting newEmailSetting = new Setting
            {
                Key = EmailConfig.Key,
                Content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emailConfig))
            };
            await unitOfWork.Settings.AddAsync(newEmailSetting, cancellationToken);
            return true;
        }
    }
}
