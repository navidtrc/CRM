using FluentValidation;

namespace CRM.Application.SecurityApplication.Authentication.Logout
{
    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(r => r.UserId).NotNull().WithMessage("UserId is required");
            RuleFor(r => r.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}
