using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.TicketApplication.Commands.NextInvoiceNumber
{
    public class NextInvoiceNumberCommand : IRequest<OperationResult<int>>
    {
    }
}
