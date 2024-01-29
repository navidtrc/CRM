using CRM.Common.Api;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.UI;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Ticket
{
    public interface ITicketService
    {
        Task<ResultContent<DataSourceResult>> GetTicketsAsync(DataSourceRequest request, CancellationToken cancellationToken);
        Task<ResultContent<TicketPrerequisiteViewModel>> PrerequisiteAsync(CancellationToken cancellationToken);
        Task<ResultContent<TicketPrerequisiteViewModel>> CreateAsync(TicketAddEditViewModel ticketAddEditViewModel, CancellationToken cancellationToken);
    }
}