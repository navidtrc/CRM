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
        public async Task<IRestResponse> SendSmsAsync(string sendTo, string text, CancellationToken cancellationToken)
        {
            var smsSetting = await unitOfWork.Settings.TableNoTracking.FirstOrDefaultAsync(f => f.Key == SmsConfig.Key, cancellationToken);
            var options = JsonConvert.DeserializeObject<SmsConfig>(Encoding.UTF8.GetString(smsSetting.Content));

            var client = new RestClient(options.Url);
            client.Timeout = -1;
            var request = new RestRequest(options.Method);
            request.AddHeader("Content-Type", options.ContentType);
            request.AddParameter("userName", options.UserName);
            request.AddParameter("password", options.Password);
            request.AddParameter("to", sendTo);
            request.AddParameter("from", options.SendFrom);
            request.AddParameter("text", text);
            request.AddParameter("isFlash", options.IsFlash.ToString());
            IRestResponse response = await client.ExecuteAsync(request, cancellationToken);
            return response;
        }
    }
}
