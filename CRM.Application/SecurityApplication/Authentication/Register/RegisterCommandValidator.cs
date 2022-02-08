using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Authentication.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(r => r.Email).EmailAddress().WithMessage("Email is not correct");
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(r => r.Gender).NotEmpty().WithMessage("Gender is required");
            RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required");
            RuleFor(m => m.ConfirmPassword).Must((model, field) => model.Password == field ).WithMessage("Password is not match");
        }
    }
}
