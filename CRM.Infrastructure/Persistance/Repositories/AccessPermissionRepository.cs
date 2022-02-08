using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class AccessPermissionRepository : Repository<AccessPermission>, IAccessPermissionRepository
    {
        public AccessPermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
