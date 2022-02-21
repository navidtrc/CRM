using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.Services.Sms
{
    public interface ISmsService
    {
        Task<IRestResponse> SendAsync(Method method, string message, string[] receivers, CancellationToken cancellationToken);
        Task<IRestResponse> SendWithPatternAsync(Method method, string patternName, Dictionary<string, string> keyValuePairs, string[] receivers, CancellationToken cancellationToken);
    }
}
