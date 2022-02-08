using Common;
using Common.Enums;
using CRM.Application.Common;
using MediatR;

namespace CRM.Application.SecurityApplication.Authentication.EmailConfirmation
{
    public class EmailConfirmationCommand : IRequest<OperationResult>, ICommittableRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
