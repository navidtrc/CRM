using CRM.Infrastructure.Persistance.Core;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.Services.Sms
{
    public class SmsService : ISmsService
    {
        private readonly IUnitOfWork uow;
        public SmsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<IRestResponse> SendAsync(Method method, string message, string[] receivers, CancellationToken cancellationToken)
        {
            var dbConfig = uow.Lookup.GetTypeValues("SmsSetting").Data.FirstOrDefault();
            var configs = JsonConvert.DeserializeObject<SmsSetting>(dbConfig.Aux1);
            var client = new RestClient("http://188.0.240.110/api/select");
            var request = new RestRequest(method);
            client.Timeout = -1;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("cache-control", "no-cache");
            var body = new
            {
                op = configs.op,
                uname = configs.uname,
                pass = configs.pass,
                message = message,
                from = configs.from,
                to = receivers
            };
            var jsonString = JsonConvert.SerializeObject(body);
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request, cancellationToken);
            return response;
        }
        public async Task<IRestResponse> SendWithPatternAsync(Method method, string patternName, Dictionary<string, string> keyValuePairs, string[] receivers, CancellationToken cancellationToken)
        {
            var content = uow.Lookup.GetTypeValues(patternName).Data.FirstOrDefault().Aux1;
            foreach (var item in keyValuePairs)
                content = content.Replace(item.Key, item.Value);
            return await SendAsync(method, content, receivers, cancellationToken);
        }
    }
}