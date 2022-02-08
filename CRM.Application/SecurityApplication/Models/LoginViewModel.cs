using Common.Enums;
using CRM.Application.SecurityApplication.Authentication.Login;
using CRM.Application.WebFramework.Api;
using CRM.Application.WebFramework.Filters;
using System.ComponentModel.DataAnnotations;

namespace CRM.Application.SecurityApplication.Models
{
    public class LoginViewModel : BaseViewModel<LoginViewModel, LoginCommand>
    {
        [Required(ErrorMessage = "Select is INTERNAL or EXTERNAL login type")]
        public eUserFrom UserFrom { get; set; }

        [Required(ErrorMessage = "Please enter email or phone number")]
        [EmailOrPhone(ErrorMessage = "Please enter a valid email address or phone number")]
        public string EmailOrPhone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        public bool AppendCookie { get; set; }
    }
}
