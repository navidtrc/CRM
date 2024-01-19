using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class RecoveryRepository : Repository<Entities.DataModels.Security.Recovery>, IRecoveryRepository
    {
        public RecoveryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
