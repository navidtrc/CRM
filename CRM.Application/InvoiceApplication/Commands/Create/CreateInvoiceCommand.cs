using Common;
using CRM.Domain.Models.Ticket;
using MediatR;

namespace CRM.Application.InvoiceApplication.Commands.Create
{
    public class CreateInvoiceCommand : IRequest<OperationResult>
    {
        public Invoice ViewModel { get; set; }
    }
}
