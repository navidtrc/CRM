using AutoMapper;
using Common;
using CRM.Application.Services.Sms;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.InvoiceApplication.Commands.Update
{
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, OperationResult>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ISmsService smsService;

        public UpdateInvoiceCommandHandler(IUnitOfWork uow, IMapper mapper, ISmsService smsService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.smsService = smsService;
        }
        public async Task<OperationResult> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var vm = request.ViewModel.ToEntity(mapper);
            var invoice = await uow.Invoices.TableNoTracking
                .Include(f => f.Customer)
                .FirstOrDefaultAsync(f => f.Id == vm.Id);

            if (vm.StateId != invoice.StateId)
            {
                var newState = await uow.Lookup.GetByIdAsync(cancellationToken, vm.StateId);
                var receivers = new string[1] { invoice.Customer.PhoneNumber.Remove(0, 1) };
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("{0}", $"{invoice.Customer.FirstName} {invoice.Customer.LastName}");
                keyValuePairs.Add("{1}", invoice.Number);
                keyValuePairs.Add("{2}", newState.Aux2);
                await smsService.SendWithPatternAsync(Method.POST, "InvoiceStateChangeSmsContent", keyValuePairs, receivers, cancellationToken);
            }

            await uow.Invoices.UpdateAsync(vm, cancellationToken);
            var result = await uow.CompleteAsync(cancellationToken);
            return new OperationResult(true);
        }
    }
}
