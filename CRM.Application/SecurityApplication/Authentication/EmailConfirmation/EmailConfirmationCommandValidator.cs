using FluentValidation;

namespace CRM.Application.SecurityApplication.Authentication.EmailConfirmation
{
    public class EmailConfirmationCommandValidator : AbstractValidator<EmailConfirmationCommand>
    {
        public EmailConfirmationCommandValidator()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(r => r.Email).EmailAddress().WithMessage("Email format wrong");
            RuleFor(r => r.Code).NotEmpty().WithMessage("Code is required");
        }
    }
}
