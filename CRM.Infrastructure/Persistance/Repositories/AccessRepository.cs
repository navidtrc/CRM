using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class AccessRepository : Repository<Access>, IAccessRepository
    {
        public AccessRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
