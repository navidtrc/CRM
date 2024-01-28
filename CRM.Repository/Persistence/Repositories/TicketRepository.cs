using CRM.DAL;
using CRM.Repository.Core.Repositories;
using CRM.Entities.DataModels.Basic;

namespace CRM.Repository.Persistence.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
