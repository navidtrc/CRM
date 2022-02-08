using CRM.Domain.Models.Ticket.TicketTypeModels;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class TicketTypeBaseModelRepository : Repository<TicketTypeBaseModel>, ITicketTypeBaseModelRepository
    {
        public TicketTypeBaseModelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
