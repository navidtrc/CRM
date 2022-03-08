using AutoMapper;
using CRM.Application.LookupApplication.Queries.FindType;
using CRM.Application.LookupApplication.ViewModels;
using CRM.Application.WebFramework.Api;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Web.Controllers
{
    [ApiController]
    [Route("api/lookup")]
    public class LookupController : BaseController
    {
        private readonly IUnitOfWork uow;

        public LookupController(IMediator mediator, IMapper mapper, IUnitOfWork uow) : base(mediator, mapper)
        {
            this.uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetLookupTypeValuesViewModel vm, CancellationToken cancellationToken)
        {
            var result2 = await _mediator.Send(new LookupGetTypeValuesCommand() { Types = vm.Types }, cancellationToken);
            if (result2.IsSuccess)
                return Ok(result2);
            return BadRequest(result2.Message);
        }
    }
}