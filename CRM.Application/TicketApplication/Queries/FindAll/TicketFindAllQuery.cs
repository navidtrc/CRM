using Kendo.Mvc.UI;
using MediatR;

namespace CRM.Application.TicketApplication.Queries.FindAll
{
    public class TicketFindAllQuery : IRequest<DataSourceResult>
    {
        public DataSourceRequest Request { get; set; }
        public int TicketTypeId { get; set; }
    }
}
