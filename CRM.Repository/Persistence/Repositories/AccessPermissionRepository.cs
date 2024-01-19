using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class AccessPermissionRepository : Repository<Entities.DataModels.Security.AccessPermission>, IAccessPermissionRepository
    {
        public AccessPermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
