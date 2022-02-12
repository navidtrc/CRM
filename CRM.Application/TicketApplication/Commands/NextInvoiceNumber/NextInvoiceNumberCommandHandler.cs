using Common;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.TicketApplication.Commands.NextInvoiceNumber
{
    public class NextInvoiceNumberCommandHandler : IRequestHandler<NextInvoiceNumberCommand, OperationResult<int>>
    {
        private readonly IUnitOfWork uow;

        public NextInvoiceNumberCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<OperationResult<int>> Handle(NextInvoiceNumberCommand request, CancellationToken cancellationToken)
        {
            var lastNumber = uow.Invoices.TableNoTracking.OrderByDescending(f => f.Number).FirstOrDefault().Number;
            return new OperationResult<int>(true, int.Parse(lastNumber) + 1);
        }

    }
}
