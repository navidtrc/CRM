using AutoMapper;
using Common;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.InvoiceApplication.Commands.Delete
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, OperationResult>
    {
        private readonly IUnitOfWork uow;

        public DeleteInvoiceCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<OperationResult> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await uow.Invoices.GetByIdAsync(cancellationToken, request.Id);
            await uow.Invoices.DeleteAsync(invoice, cancellationToken);
            await uow.CompleteAsync(cancellationToken);
            return new OperationResult(true);
        }
    }
}
