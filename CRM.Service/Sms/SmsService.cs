using CRM.Entities.HelperModels;
using CRM.Repository.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Sms
{
    public class SmsService : ISmsService
    {
        private readonly IUnitOfWork unitOfWork;
        public SmsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<RestResponse> SendSmsAsync(string sendTo, string text, CancellationToken cancellationToken)
        {
            var smsSetting = await unitOfWork.Settings.TableNoTracking.FirstOrDefaultAsync(f => f.Key == SmsConfig.Key, cancellationToken);
            var options = JsonConvert.DeserializeObject<SmsConfig>(Encoding.UTF8.GetString(smsSetting.Content));

            var client = new RestClient(options.Url);
            //client.Timeout = -1;
            var request = new RestRequest();
            request.AddHeader("Content-Type", options.ContentType);
            request.AddParameter("userName", options.UserName);
            request.AddParameter("password", options.Password);
            request.AddParameter("to", sendTo);
            request.AddParameter("from", options.SendFrom);
            request.AddParameter("text", text);
            request.AddParameter("isFlash", options.IsFlash.ToString());
            var response = await client.PostAsync(request, cancellationToken);
            return response;
        }
    }
}
