using CRM.Entities.HelperModels;
using CRM.ViewModels.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Email
{
    public interface IEmailService
    {
        Task<bool> ComposeAsync(ComposeEmailViewModel emailSetting, CancellationToken cancellationToken);
        Task<bool> SendAsync(string sendTo, string subject, string body, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(EmailConfig emailConfig, CancellationToken cancellationToken);
    }
}