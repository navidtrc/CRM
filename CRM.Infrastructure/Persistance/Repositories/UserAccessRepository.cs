using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class UserAccessRepository : Repository<UserAccess>, IUserAccessRepository
    {
        public UserAccessRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
