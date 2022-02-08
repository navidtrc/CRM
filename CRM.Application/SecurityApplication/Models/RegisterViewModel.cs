using Common.Enums;
using CRM.Application.SecurityApplication.Authentication.Register;
using CRM.Application.WebFramework.Api;
using CRM.Domain.Common.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CRM.Application.SecurityApplication.Models
{
    public class RegisterViewModel : BaseViewModel<RegisterViewModel, RegisterCommand>
    {
        [Required(ErrorMessage = "Select INTERNAL or EXTERNAL login type")]
        public eUserFrom UserFrom { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not correct")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public eGender Gender { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not match")]
        public string ConfirmPassword { get; set; }

        public string ProfileImage { get; set; }
    }
}
