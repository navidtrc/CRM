using Common;
using CRM.Application.InvoiceApplication.ViewModels;
using CRM.Application.LookupApplication.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.InvoiceApplication.Queries.GetById
{
    public class GetInvoiceByIdCommand : IRequest<OperationResult<InvoiceViewModel>>
    {
        public int Id { get; set; }
    }
}
