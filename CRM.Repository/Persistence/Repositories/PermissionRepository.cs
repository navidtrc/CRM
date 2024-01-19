using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class PermissionRepository : Repository<Entities.DataModels.Security.Permission>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
