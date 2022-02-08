using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using CRM.Application.SecurityApplication.Models;
using CRM.Application.WebFramework.Api;
using CRM.Application.WebFramework.Filters;
using Common.Enums;
using CRM.Domain.Common.Enums;
using CRM.Domain.Models.Security;
using AutoMapper;
using CRM.Application.SecurityApplication.Authentication.Logout;
using System.Security.Claims;
using System;

namespace CRM.UI.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Login(LoginViewModel vm, CancellationToken cancellationToken)
        {
            var command = vm.ToEntity(_mapper);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Register(RegisterViewModel vm, CancellationToken cancellationToken)
        {
            var command = vm.ToEntity(_mapper);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel vm, CancellationToken cancellationToken)
        {
            var command = vm.ToEntity(_mapper);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel vm, CancellationToken cancellationToken)
        {
            var command = vm.ToEntity(_mapper);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm, CancellationToken cancellationToken)
        {
            var command = vm.ToEntity(_mapper);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            if (!UserIsAutheticated)
                return BadRequest();
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _mediator.Send(new LogoutCommand() { UserId = userId }, cancellationToken);
            if (result.IsSuccess)
                return SignOut();
            return BadRequest(result.Message);
        }
    }
}