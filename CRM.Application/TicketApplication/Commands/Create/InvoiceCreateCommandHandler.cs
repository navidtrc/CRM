using Common;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.TicketApplication.Commands.Create
{
    public class InvoiceCreateCommandHandler : IRequestHandler<InvoiceCreateCommand, OperationResult>
    {
        private readonly IUnitOfWork uow;

        public InvoiceCreateCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<OperationResult> Handle(InvoiceCreateCommand request, CancellationToken cancellationToken)
        {

            var customer = new Domain.Models.Security.Customer
            {
                FirstName = request.firstName,
                LastName = request.lastName,
                PhoneNumber = request.phoneNumber,
            };
            await uow.Customers.AddAsync(customer, cancellationToken);

            var invoice = new Domain.Models.Ticket.Invoice
            {
                Number = request.number,
                Date = request.date,
                State = request.state,
                CustomerId = customer.Id
            };
            await uow.Invoices.AddAsync(invoice, cancellationToken);

            foreach (var device in request.devices)
            {
                await uow.Devices.AddAsync(new Domain.Models.Ticket.Device
                {
                    Type = device.type,
                    Brand = device.brand,
                    Model = device.model,
                    Accessories = device.accessories,
                    Description = device.description,
                    ShopPrice = device.shopPrice,
                    CustomerPrice= device.customerPrice,
                    ShopWarranty = device.shopWarranty,
                    RepairWarranty= device.repairWarranty,
                    InvoiceId = invoice.Id
                }, cancellationToken);
            }
            await uow.CompleteAsync(cancellationToken);
            return new OperationResult(true);
        }
    }
}
