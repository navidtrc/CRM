using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM.ViewModels.ViewModels;
using CRM.Service.User;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CRM.WebFramework.Filters;

namespace CRM.UI.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(Common.Enums.eAccessControl.AccountLoginApi, Common.Enums.eAccessType.Api, 0, true)]
        public async Task<IActionResult> Login(LoginViewModel userViewModel, CancellationToken cancellationToken)
        {
            var result = await userService.Login(userViewModel, cancellationToken);
            if (result.IsSuccess)
            {
                if (userViewModel.AppendCookie)
                    HttpContext.Response.Cookies.Append("access_token", result.Data, new CookieOptions { HttpOnly = true });
                return Ok(result.Data);
            }
            return BadRequest();
        }

        //[HttpPost]
        //[Route("/api/Account/Register")]
        //[UserAccess(Common.Enums.eAccessControl.AccountRegisterApi, Common.Enums.eAccessType.Api, 1, true)]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel, CancellationToken cancellationToken)
        //{
        //    registerViewModel.Person.ePersonType = Common.Enums.ePersonType.Staff;
        //    var result = await userService.Register(registerViewModel, cancellationToken);
        //    if (result.IsSuccess)
        //        return Ok(result.Message);
        //    return BadRequest(result.Message);
        //}

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(Common.Enums.eAccessControl.AccountLogoutApi, Common.Enums.eAccessType.Api, 2)]
        [Authorize]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var result = await userService.Logout(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), cancellationToken);
            return RedirectToAction("Login", "/Account");
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(Common.Enums.eAccessControl.AccountForgetPasswordApi, Common.Enums.eAccessType.Api, 3)]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel, CancellationToken cancellationToken)
        {
            var result = await userService.ForgetPassword(forgetPasswordViewModel, Request, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(Common.Enums.eAccessControl.AccountForgetPasswordConfirmApi, Common.Enums.eAccessType.Api, 4)]
        public async Task<IActionResult> ForgetPasswordConfirm(ForgetPasswordConfirmViewModel forgetPasswordConfirmViewModel, CancellationToken cancellationToken)
        {
            var result = await userService.ForgetPasswordConfirm(forgetPasswordConfirmViewModel, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(Common.Enums.eAccessControl.AccountForgetPasswordConfirmApi, Common.Enums.eAccessType.Api, 4)]
        public async Task<IActionResult> Lockout(UserLockoutViewModel userLockoutViewModel, CancellationToken cancellationToken)
        {
            var result = await userService.Lockout(userLockoutViewModel, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(Common.Enums.eAccessControl.AccountForgetPasswordConfirmApi, Common.Enums.eAccessType.Api, 4)]
        public async Task<IActionResult> SendCode(SendCodeViewModel sendCodeViewModel, CancellationToken cancellationToken)
        {
            var result = await userService.SendCode(sendCodeViewModel, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(Common.Enums.eAccessControl.AccountForgetPasswordConfirmApi, Common.Enums.eAccessType.Api, 4)]
        public async Task<IActionResult> Confirmation(ConfirmCodeViewModel confirmCodeViewModel, CancellationToken cancellationToken)
        {
            var result = await userService.Confirmation(confirmCodeViewModel, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
    }
}