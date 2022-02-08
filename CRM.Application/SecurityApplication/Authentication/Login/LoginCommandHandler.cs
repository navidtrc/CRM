using Common;
using Common.Enums;
using Common.Utilities;
using CRM.Application.SecurityApplication.Models;
using CRM.Application.SecurityApplication.Services.Authentication;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using EmailSender.Models;
using EmailSender.Service;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Authentication.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<AccessTokenViewModel>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailSender _emailSender;

        public LoginCommandHandler(IUnitOfWork uow,
            IJwtService jwtService,
            SignInManager<User> signInManager,
            IPasswordHasher<User> passwordHasher,
            IEmailSender emailSender)
        {
            _uow = uow;
            _jwtService = jwtService;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _emailSender = emailSender;
        }

        public async Task<OperationResult<AccessTokenViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            eLoginType loginType = request.EmailOrPhone.Contains("@") ? eLoginType.Email : eLoginType.PhoneNumber;
            User user = loginType == eLoginType.Email ?
                _uow.Users.Table.Include(i => i.Person).FirstOrDefault(f => f.Email == request.EmailOrPhone) :
                _uow.Users.Table.Include(i => i.Person).FirstOrDefault(f => f.PhoneNumber == request.EmailOrPhone);

            //var s1 = new Specs.EmailVerificationSpec().IsSatisfiedBy(user);
            //var s2 = new Specs.UserIsActiveSpec().IsSatisfiedBy(user);
            //var s3 = s1.And(s2);
            //var result = s3.IsSatisfiedBy(user);

            //if (!result)
            //{
            //    if (errorMessages.Contains("Email is not verified"))
            //    {
            //        var confirmationCode = CodeGenerator.Generate();
            //        user.ConfirmationCodeHash = SecurityHelper.GetSha256Hash(confirmationCode);

            //        await _emailSender.SendEmailAsync(new Message(new string[] { user.Email }, "New email confirmation", $"Code: {confirmationCode}", null));
            //        await _uow.Users.UpdateAsync(user, cancellationToken);
            //        await _uow.CompleteAsync(cancellationToken);
            //        return new OperationResult<AccessTokenViewModel>(false, null, "Check your email to verify it");
            //    }
            //    return new OperationResult<AccessTokenViewModel>(false, null, errorMessages);
            //}

            if (request.UserFrom == eUserFrom.External)
                request.Password = SecurityHelper.PasswordGeneratorBasedOnKey($"{user.Email}-{user.Person.FirstName}-{user.Person.LastName}-{user.Guid}", "hQvt~rVH4R964,[!");

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (signInResult.Succeeded)
                return new OperationResult<AccessTokenViewModel>(true, await _jwtService.GenerateAsync(user));
            
            return new OperationResult<AccessTokenViewModel>(false, null);
        }
    }
}
