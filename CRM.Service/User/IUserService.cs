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
        Task<ResultContent<string>> LoginAsync(LoginViewModel loginViewModel, CancellationToken cancellationToken);
        //Task<ResultContent> Register(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken);
        Task<ResultContent> LogoutAsync(Guid id, CancellationToken cancellationToken);
        Task<ResultContent> ForgetPasswordAsync(ForgetPasswordViewModel forgetPasswordViewModel, HttpRequest request, CancellationToken cancellationToken);
        Task<ResultContent> ForgetPasswordConfirmAsync(ForgetPasswordConfirmViewModel forgetPasswordConfirmViewModel, CancellationToken cancellationToken);
        DataSourceResult GetAllUsers_DataSourceResult(DataSourceRequest request);
        UserInfoViewModel GetCurrentUser();
        Task<ResultContent> SendCodeAsync(SendCodeViewModel sendCodeViewModel, CancellationToken cancellationToken);
        Task<ResultContent> ConfirmationAsync(ConfirmCodeViewModel confirmCodeViewModel, CancellationToken cancellationToken);
        Task<ResultContent> LockoutAsync(UserLockoutViewModel userLockoutViewModel, CancellationToken cancellationToken);
        Task<ResultContent> UserAccessChangeAsync(UserAccessChangeViewModel userAccessChangeViewModel, CancellationToken cancellationToken);
    }
}