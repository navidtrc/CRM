using Kendo.Mvc.UI;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.TicketApplication.Queries.FindAll
{
    public class TicketFindAllQueryHandler : IRequestHandler<TicketFindAllQuery, DataSourceResult>
    {
        public async Task<DataSourceResult> Handle(TicketFindAllQuery request, CancellationToken cancellationToken)
        {

            return null;
        }
    }
}
