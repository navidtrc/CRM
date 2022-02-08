using Common;
using Common.Enums;
using CRM.Application.Common;
using CRM.Domain.Common.Enums;
using MediatR;

namespace CRM.Application.SecurityApplication.Authentication.Register
{
    public class RegisterCommand : IRequest<OperationResult>, ICommittableRequest
    {
        public eUserFrom UserFrom { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public eGender Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
