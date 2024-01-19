﻿using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Sms
{
    public interface ISmsService
    {
        Task<IRestResponse> SendSmsAsync(string sendTo, string text, CancellationToken cancellationToken);
    }
}