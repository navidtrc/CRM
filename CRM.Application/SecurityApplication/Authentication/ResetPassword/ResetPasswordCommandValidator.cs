using FluentValidation;

namespace CRM.Application.SecurityApplication.Authentication.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(r => r.ResetCode).NotEmpty().WithMessage("Code is required");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required");
            RuleFor(m => m.ConfirmPassword).Must((model, field) => model.Password == field).WithMessage("Password is not match");
        }
    }
}
