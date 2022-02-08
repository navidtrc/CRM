using CRM.Domain.Models.Ticket;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class TicketFlowRepository : Repository<TicketFlow>, ITicketFlowRepository
    {
        public TicketFlowRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
