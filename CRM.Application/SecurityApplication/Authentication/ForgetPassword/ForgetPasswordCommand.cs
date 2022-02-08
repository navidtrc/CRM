using Common;
using Common.Enums;
using CRM.Application.Common;
using MediatR;
using System;

namespace CRM.Application.SecurityApplication.Authentication.ForgetPassword
{
    public class ForgetPasswordCommand : IRequest<OperationResult<string>>, ICommittableRequest
    {
        public eLoginType ForgetType { get; set; }
        public string EmailOrPhone { get; set; }
    }
}
