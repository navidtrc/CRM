using CRM.Application.SecurityApplication.Authentication.ResetPassword;
using CRM.Application.WebFramework.Api;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Application.SecurityApplication.Models
{
    public class ResetPasswordViewModel : BaseViewModel<ResetPasswordViewModel, ResetPasswordCommand>
    {

        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }


        [Required(ErrorMessage = "Code is required")]
        public string ResetCode { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not match")]
        public string ConfirmPassword { get; set; }
    }
}
