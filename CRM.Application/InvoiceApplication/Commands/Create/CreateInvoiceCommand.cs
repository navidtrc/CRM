using Common;
using CRM.Application.InvoiceApplication.ViewModels;
using MediatR;

namespace CRM.Application.InvoiceApplication.Commands.Create
{
    public class CreateInvoiceCommand : IRequest<OperationResult>
    {
        public InvoiceViewModel ViewModel { get; set; }
    }
}
