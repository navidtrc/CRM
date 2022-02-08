using Common;
using Common.Enums;
using CRM.Application.SecurityApplication.Models;
using MediatR;

namespace CRM.Application.SecurityApplication.Authentication.Login
{
    public class LoginCommand : IRequest<OperationResult<AccessTokenViewModel>>
    {
        public eUserFrom UserFrom { get; set; }
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }
}
