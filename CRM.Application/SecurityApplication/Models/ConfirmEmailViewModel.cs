using CRM.Application.SecurityApplication.Authentication.EmailConfirmation;
using CRM.Application.WebFramework.Api;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CRM.Application.SecurityApplication.Models
{
    public class ConfirmEmailViewModel : BaseViewModel<ConfirmEmailViewModel, EmailConfirmationCommand>
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Code { get; set; }
    }
}
