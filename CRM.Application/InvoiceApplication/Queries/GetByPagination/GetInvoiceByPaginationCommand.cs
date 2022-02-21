using Common;
using Common.Enums;
using CRM.Application.InvoiceApplication.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace CRM.Application.InvoiceApplication.Queries.GetByPagination
{

    public class GetInvoiceByPaginationCommand : IRequest<OperationResult<List<InvoiceViewModel>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filters { get; set; }
        public Sort Sort { get; set; }
    }
}
