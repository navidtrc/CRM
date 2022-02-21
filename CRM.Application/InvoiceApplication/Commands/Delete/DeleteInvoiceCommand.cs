using Common;
using MediatR;

namespace CRM.Application.InvoiceApplication.Commands.Delete
{
    public class DeleteInvoiceCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
