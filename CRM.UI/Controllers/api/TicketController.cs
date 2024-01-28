using CRM.Common.Enums;
using CRM.Service.People;
using CRM.Service.Ticket;
using CRM.ViewModels.ViewModels;
using CRM.WebFramework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.UI.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticketService;
        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(eAccessControl.TicketGetList, eAccessType.Api, 0, true)]
        public async Task<IActionResult> Get(MaterialDataGridQueryViewModel request, CancellationToken cancellationToken)
        {
            //ePersonType personType = (ePersonType)Enum.Parse(typeof(ePersonType), request.type, true);
            //var response = await ticketService.GetAsync(request.ToDataSourceRequest(), personType, cancellationToken);
            //if (response.IsSuccess)
            //    return Ok(response);
            //return BadRequest(response.Message);
            return Ok();
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(eAccessControl.TicketGetList, eAccessType.Api, 0, true)]
        public async Task<IActionResult> Prerequisite(MaterialDataGridQueryViewModel request, CancellationToken cancellationToken)
        {
            //var response = await ticketService.Prerequisite
            return Ok();
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        [UserAccess(eAccessControl.PeopleRegisterApi, eAccessType.Api, 1, true)]
        public async Task<IActionResult> Post(TicketAddEditViewModel ticketAddEditViewModel, CancellationToken cancellationToken)
        {
            //var result = await ticketService.Create(registerViewModel, cancellationToken);
            //if (result.IsSuccess)
            //    return Ok(result.Message);
            //return BadRequest(result.Message);
            return Ok();
        }


        //[HttpPost]
        //[Route("/api/[controller]/[action]")]
        //[UserAccess(eAccessControl.PeopleRegisterApi, eAccessType.Api, 1, true)]
        //public async Task<IActionResult> Post(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken)
        //{
        //    var result = await ticketService.Create(registerViewModel, cancellationToken);
        //    if (result.IsSuccess)
        //        return Ok(result.Message);
        //    return BadRequest(result.Message);
        //}

        //[HttpPut]
        //[Route("/api/[controller]/[action]")]
        //[UserAccess(eAccessControl.PeopleRegisterApi, eAccessType.Api, 1, true)]
        //public async Task<IActionResult> Put(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken)
        //{
        //    var result = await ticketService.Put(registerViewModel, cancellationToken);
        //    if (result.IsSuccess)
        //        return Ok(result.Message);
        //    return BadRequest(result.Message);
        //}


        //[HttpDelete]
        //[Route("/api/[controller]/[action]")]
        //[UserAccess(eAccessControl.PeopleDelete, eAccessType.Api, 1, true)]
        //public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        //{
        //    var response = await ticketService.DeleteAsync(id, cancellationToken);
        //    if (response.IsSuccess)
        //        return Ok();
        //    return BadRequest(response.Message);
        //}
    }
}