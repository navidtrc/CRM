using AutoMapper;
using CRM.Application.InvoiceApplication.Commands.Create;
using CRM.Application.InvoiceApplication.Commands.Delete;
using CRM.Application.InvoiceApplication.Commands.Update;
using CRM.Application.InvoiceApplication.Queries.GetById;
using CRM.Application.InvoiceApplication.Queries.GetByPagination;
using CRM.Application.InvoiceApplication.Queries.GetNextInvoiceNumber;
using CRM.Application.InvoiceApplication.ViewModels;
using CRM.Application.WebFramework.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.UI.Controllers
{
    [ApiController]
    public class InvoiceController : BaseController
    {
        public InvoiceController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetLastInvoiceNumber(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetNextInvoiceNumberCommand(), cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInvoiceByIdCommand { Id = id }, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetByPagination(PaginationRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInvoiceByPaginationCommand
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Filters = request.Filters,
                Sort = request.Sort
            }, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Post(InvoiceViewModel invoice, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateInvoiceCommand { ViewModel = invoice }, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Put(InvoiceViewModel invoice, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateInvoiceCommand { ViewModel = invoice }, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteInvoiceCommand { Id = id }, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}
