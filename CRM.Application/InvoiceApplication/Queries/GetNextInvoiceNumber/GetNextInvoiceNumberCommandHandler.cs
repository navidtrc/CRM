using Common;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.InvoiceApplication.Queries.GetNextInvoiceNumber
{
    public class GetNextInvoiceNumberCommandHandler : IRequestHandler<GetNextInvoiceNumberCommand, OperationResult<int>>
    {
        private readonly IUnitOfWork uow;
        public GetNextInvoiceNumberCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<OperationResult<int>> Handle(GetNextInvoiceNumberCommand request, CancellationToken cancellationToken)
        {
            var lastInvoice = await uow.Invoices.TableNoTracking.OrderByDescending(f => f.Number).FirstOrDefaultAsync(cancellationToken);
            return new OperationResult<int>(true, int.Parse(lastInvoice.Number) + 1);
        }

    }
}
