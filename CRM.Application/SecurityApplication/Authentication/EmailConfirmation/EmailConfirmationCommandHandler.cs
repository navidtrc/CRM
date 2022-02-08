using Common;
using Common.Enums;
using Common.Utilities;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using EmailSender.Models;
using EmailSender.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Authentication.EmailConfirmation
{
    public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand, OperationResult>
    {
        private readonly IUnitOfWork _uow;

        public EmailConfirmationCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<OperationResult> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = _uow.Users.Table.Single(f => f.Email == request.Email);
            var confirmationCodeHash = SecurityHelper.GetSha256Hash(request.Code);
            if (user.ConfirmationCodeHash == confirmationCodeHash)
            {
                user.IsActive = true;
                user.EmailConfirmed = true;
                user.ConfirmationCodeHash = SecurityHelper.GetSha256Hash(CodeGenerator.Generate());
                await _uow.Users.UpdateAsync(user, cancellationToken);
                return new OperationResult(true, "Email confirmed succesfully");
            }
            return new OperationResult(false, "Confirmation failed");
        }
    }
}
