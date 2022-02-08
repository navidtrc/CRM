using Common;
using Common.Enums;
using Common.Utilities;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using EmailSender.Models;
using EmailSender.Service;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OperationResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(IUnitOfWork uow, IEmailSender emailSender, UserManager<User> userManager)
        {
            _uow = uow;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (_uow.Users.TableNoTracking.Any(f => f.Email == request.Email))
                return new OperationResult(false, "User already exist");
            var confirmationCode = CodeGenerator.Generate();
            User user = new User();
            if (request.UserFrom == eUserFrom.External)
            {
                request.Password = request.ConfirmPassword
                    = SecurityHelper.PasswordGeneratorBasedOnKey($"{request.Email}-{request.FirstName}-{request.LastName}-{user.Guid}", "hQvt~rVH4R964,[!");
                user.IsActive = true;
                user.EmailConfirmed = true;
                user.RegisteredType = eUserFrom.External;
            }
            else
            {
                user.IsActive = false;
                user.EmailConfirmed = false;
                user.ConfirmationCodeHash = SecurityHelper.GetSha256Hash(confirmationCode);
            }
            user.Email = request.Email;
            user.UserName = request.Email;
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                Customer customer = new Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender,
                    UserId = user.Id
                };
                await _uow.Customers.AddAsync(customer, cancellationToken);
                await _uow.CompleteAsync(cancellationToken);
                if (request.UserFrom == eUserFrom.Internal)
                {
                    var message = new Message(new string[] { user.Email }, "Email Confirmation", $"Code: {confirmationCode}", null);
                    await _emailSender.SendEmailAsync(message);
                    return new OperationResult(true, "Check your Email for confirmation");
                }
                else
                    return new OperationResult(true, "You registered successfully");
            }
            return new OperationResult(false, string.Join(',', result.Errors));
        }
    }
}
