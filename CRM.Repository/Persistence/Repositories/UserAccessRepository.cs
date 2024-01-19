using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class UserAccessRepository : Repository<Entities.DataModels.Security.UserAccess>, IUserAccessRepository
    {
        public UserAccessRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
