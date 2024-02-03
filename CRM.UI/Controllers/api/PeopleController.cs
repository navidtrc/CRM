using CRM.Common.Enums;
using CRM.Service.People;
using CRM.ViewModels.ViewModels;
using CRM.WebFramework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.UI.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService peopleService;
        public PeopleController(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(eAccessControl.PeopleGetList, eAccessType.Api, 0, true)]
        public async Task<IActionResult> Get(MaterialDataGridQueryViewModel request, CancellationToken cancellationToken)
        {
            ePersonType personType = (ePersonType)Enum.Parse(typeof(ePersonType), request.type, true);
            var response = await peopleService.GetAsync(request.ToDataSourceRequest(), personType, cancellationToken);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }

        [HttpGet]
        [Route("/api/[controller]/[action]")]
        [UserAccess(eAccessControl.PeopleGetList, eAccessType.Api, 0, true)]
        public async Task<IActionResult> GetByRole(CancellationToken cancellationToken)
        {
            var response = await peopleService.GetByRoleAsync(cancellationToken);
            return Ok(response);
        }


        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(eAccessControl.PeopleRegisterApi, eAccessType.Api, 1, true)]
        public async Task<IActionResult> Post(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken)
        {
            var result = await peopleService.CreateAsync(registerViewModel, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPut]
        [Route("/api/[controller]/[action]")]
        [UserAccess(eAccessControl.PeopleRegisterApi, eAccessType.Api, 1, true)]
        public async Task<IActionResult> Put(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken)
        {
            var result = await peopleService.PutAsync(registerViewModel, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }


        [HttpDelete]
        [Route("/api/[controller]/[action]")]
        [UserAccess(eAccessControl.PeopleDelete, eAccessType.Api, 1, true)]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            var response = await peopleService.DeleteAsync(id, cancellationToken);
            if (response.IsSuccess)
                return Ok();
            return BadRequest(response.Message);
        }
    }
}