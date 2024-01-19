using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class RoleRepository : Repository<Entities.DataModels.Security.Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
