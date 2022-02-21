using Common;
using MediatR;

namespace CRM.Application.InvoiceApplication.Queries.GetNextInvoiceNumber
{
    public class GetNextInvoiceNumberCommand : IRequest<OperationResult<int>>
    {
    }
}
