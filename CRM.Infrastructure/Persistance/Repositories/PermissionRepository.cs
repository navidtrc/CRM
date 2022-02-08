using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
