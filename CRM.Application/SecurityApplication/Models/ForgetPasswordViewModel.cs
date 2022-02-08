using Common.Enums;
using CRM.Application.SecurityApplication.Authentication.ForgetPassword;
using CRM.Application.WebFramework.Api;
using System.ComponentModel.DataAnnotations;

namespace CRM.Application.SecurityApplication.Models
{
    public class ForgetPasswordViewModel : BaseViewModel<ForgetPasswordViewModel, ForgetPasswordCommand>
    {
        [Required(ErrorMessage = "ForgetType is required")]
        public eLoginType ForgetType { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string EmailOrPhone { get; set; }
    }
}
