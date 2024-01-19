using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class UserRoleRepository : Repository<Entities.DataModels.Security.UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
