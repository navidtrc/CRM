using FluentValidation;

namespace CRM.Application.SecurityApplication.Authentication.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(r => r.UserFrom).NotNull().WithMessage("Select INTERNAL or EXTERNAL login type");
            RuleFor(r => r.EmailOrPhone).NotEmpty().WithMessage("Please enter email or phone number");
            RuleFor(r => r.EmailOrPhone).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}").WithMessage("Please enter a valid email address or phone number");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
