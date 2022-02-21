using Common;
using CRM.Application.InvoiceApplication.ViewModels;
using MediatR;

namespace CRM.Application.InvoiceApplication.Commands.Update
{
    public class UpdateInvoiceCommand : IRequest<OperationResult>
    {
        public InvoiceViewModel ViewModel { get; set; }
    }
}
