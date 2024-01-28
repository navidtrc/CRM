using CRM.Common.Api;
using CRM.Repository.Core;
using Kendo.Mvc.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Ticket
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _uow;
        public TicketService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task<int> Prerequisite(CancellationToken cancellationToken)
        {
            var last = await _uow.Tickets.TableNoTracking.LastOrDefaultAsync(cancellationToken);
            if (last == null)
                return 1;
            var newTicketNumber = last.Number++;
            return newTicketNumber;
        }

        //public async Task<ResultContent<DataSourceResult>> GetTicketsAsync(DataSourceRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await _uow.invoice.TableNoTracking
        //                .Where(w => w.IsDeleted == false)
        //                .Include(i => i.Person.User)
        //                .ToDataSourceResultAsync(request, cancellationToken);
        //    return new ResultContent<DataSourceResult>(true, result);
        //}
    }
}
