using FluentValidation;

namespace CRM.Application.SecurityApplication.Authentication.ForgetPassword
{
    public class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
    {
        public ForgetPasswordCommandValidator()
        {
            RuleFor(r => r.ForgetType).NotNull().WithMessage("ForgetType is required");
            RuleFor(r => r.EmailOrPhone).NotEmpty().WithMessage("EmailOrPhone is required");
        }
    }
}
