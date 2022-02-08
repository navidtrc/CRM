using Common;
using CRM.Application.Common;
using MediatR;
using System;

namespace CRM.Application.SecurityApplication.Authentication.ResetPassword
{
    public class ResetPasswordCommand : IRequest<OperationResult>, ICommittableRequest
    {
        public Guid UserId { get; set; }
        public string ResetCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
