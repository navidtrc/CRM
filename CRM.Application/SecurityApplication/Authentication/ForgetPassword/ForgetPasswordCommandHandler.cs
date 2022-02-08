using Common;
using Common.Enums;
using Common.Exceptions;
using Common.Utilities;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using EmailSender.Models;
using EmailSender.Service;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Authentication.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, OperationResult<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSender _emailSender;

        public ForgetPasswordCommandHandler(IUnitOfWork uow, IEmailSender emailSender)
        {
            _uow = uow;
            _emailSender = emailSender;
        }

        public async Task<OperationResult<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = null;
            switch (request.ForgetType)
            {
                case eLoginType.Email:
                    user = _uow.Users.Table.FirstOrDefault(f => f.Email == request.EmailOrPhone);
                    if (user == null)
                        throw new NotFoundException("User is not find");
                    break;
                case eLoginType.PhoneNumber:
                    user = _uow.Users.Table.FirstOrDefault(f => f.PhoneNumber == request.EmailOrPhone);
                    if (user == null)
                        throw new NotFoundException("User is not find");
                    break;
                default:
                    break;
            }
            var confirmationCode = CodeGenerator.Generate();
            user.ConfirmationCodeHash = SecurityHelper.GetSha256Hash(confirmationCode);
            await _uow.Users.UpdateAsync(user, cancellationToken);
            switch (request.ForgetType)
            {
                case eLoginType.Email:
                    var message = new Message(new string[] { user.Email }, "Reset Password", $"Code: {confirmationCode}", null);
                    await _emailSender.SendEmailAsync(message);
                    break;
                case eLoginType.PhoneNumber:

                    break;
                default:
                    break;
            }
            return new OperationResult<string>(true, user.Id, "Code sent for reset password");
        }


    }
}
