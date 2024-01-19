using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class UserPermissionRepository : Repository<Entities.DataModels.Security.UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
