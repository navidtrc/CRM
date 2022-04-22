using AutoMapper;
using Common;
using CRM.Application.Services.Sms;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.InvoiceApplication.Commands.Create
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, OperationResult>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ISmsService smsService;

        public CreateInvoiceCommandHandler(IUnitOfWork uow, IMapper mapper, ISmsService smsService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.smsService = smsService;
        }
        public async Task<OperationResult> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = request.ViewModel;
            await uow.Invoices.AddAsync(invoice, cancellationToken);
            await uow.CompleteAsync(cancellationToken);

            var receivers = new string[1] { invoice.Customer.PhoneNumber.Remove(0, 1) };
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("{0}", $"{invoice.Customer.FirstName} {invoice.Customer.LastName}");
            keyValuePairs.Add("{1}", invoice.Number);
            await smsService.SendWithPatternAsync(Method.POST, "InvoiceCreateSmsContent", keyValuePairs, receivers, cancellationToken);

            return new OperationResult(true);
        }
    }
}
