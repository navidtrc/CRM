using CRM.Common.Api;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.User
{
    public interface IUserService
    {
        Task<ResultContent<string>> Login(LoginViewModel loginViewModel, CancellationToken cancellationToken);
        //Task<ResultContent> Register(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken);
        Task<ResultContent> Logout(Guid id, CancellationToken cancellationToken);
        Task<ResultContent> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel, HttpRequest request, CancellationToken cancellationToken);
        Task<ResultContent> ForgetPasswordConfirm(ForgetPasswordConfirmViewModel forgetPasswordConfirmViewModel, CancellationToken cancellationToken);
        DataSourceResult GetAllUsers_DataSourceResult(DataSourceRequest request);
        UserInfoViewModel GetCurrentUser();
        Task<ResultContent> SendCode(SendCodeViewModel sendCodeViewModel, CancellationToken cancellationToken);
        Task<ResultContent> Confirmation(ConfirmCodeViewModel confirmCodeViewModel, CancellationToken cancellationToken);
        Task<ResultContent> Lockout(UserLockoutViewModel userLockoutViewModel, CancellationToken cancellationToken);
    }
}