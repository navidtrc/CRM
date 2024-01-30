using CRM.DAL;
using CRM.Repository.Core.Repositories;
using CRM.Entities.DataModels.Basic;

namespace CRM.Repository.Persistence.Repositories
{
    public class FellowRepository : Repository<TicketFellow>, IFellowRepository
    {
        public FellowRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
