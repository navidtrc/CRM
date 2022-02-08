using Common;
using MediatR;
using System;

namespace CRM.Application.SecurityApplication.Authentication.Logout
{
    public class LogoutCommand : IRequest<OperationResult>
    {
        public Guid UserId { get; set; }    
    }
}
