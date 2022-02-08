using CRM.Domain.Models.Ticket;
using CRM.Infrastructure.Persistance.Core;

namespace CRM.Infrastructure.Persistance.Repositories.Core
{
    public interface ITicketRepository : IRepository<Ticket>
    {
    }
}
