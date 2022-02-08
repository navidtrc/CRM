using Common;
using Common.Utilities;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Authentication.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, OperationResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;

        public ResetPasswordCommandHandler(IUnitOfWork uow, UserManager<User> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<OperationResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.GetByIdAsync(cancellationToken, request.UserId);
            if (user.ConfirmationCodeHash == SecurityHelper.GetSha256Hash(request.ResetCode))
            {
                user.PasswordHash = SecurityHelper.GetSha256Hash(request.Password);
                await _userManager.UpdateSecurityStampAsync(user);
                return new OperationResult(true, "Password changed succesfully");
            }
            return new OperationResult(false, "Password changing failed");
        }
    }
}
