using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class AccessRoleRepository : Repository<Entities.DataModels.Security.AccessRole>, IAccessRoleRepository
    {
        public AccessRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
