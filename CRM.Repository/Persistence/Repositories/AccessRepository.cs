using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class AccessRepository : Repository<Entities.DataModels.Security.Access>, IAccessRepository
    {
        public AccessRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
