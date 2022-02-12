using AutoMapper;
using CRM.Application.TicketApplication.Commands.Create;
using CRM.Application.TicketApplication.Commands.NextInvoiceNumber;
using CRM.Application.TicketApplication.Models;
using CRM.Application.WebFramework.Api;
using CRM.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.UI.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : BaseController
    {
        public TicketController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post(InvoicePostViewModel viewModel, CancellationToken cancellationToken)
        {


            var devices = new List<DeviceVM>();
            foreach (var device in viewModel.devices)
            {
                devices.Add(new DeviceVM
                {
                    type = device.type,
                    brand = device.brand,
                    model = device.model,
                    accessories = device.accessories,
                    description = device.description,
                    customerPrice = device.customerPrice,
                    shopPrice = device.shopPrice,
                    repairWarranty = device.repairWarranty,
                    shopWarranty = device.shopWarranty,
                    state = device.state,
                });
            }
            PersianCalendar persianCalendar = new PersianCalendar();
            var persianDateArray = viewModel.date.Split('/');
            var year = int.Parse(persianDateArray[0]);
            var month = int.Parse(persianDateArray[1]);
            var day = int.Parse(persianDateArray[2]);
            var miladiDate = persianCalendar.ToDateTime(year, month, month, 0, 0, 0, 0);

            var command = new InvoiceCreateCommand
            {
                number = viewModel.number,
                date = miladiDate,
                state = (eInvoiceState)viewModel.state,
                firstName = viewModel.firstName,
                lastName = viewModel.lastName,
                phoneNumber = viewModel.phoneNumber,
                devices = devices
            };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("api/ticket/getlastinvoicenumber")]
        public async Task<IActionResult> GetLastInvoiceNumber(InvoicePostViewModel viewModel, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new NextInvoiceNumberCommand(), cancellationToken);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}
