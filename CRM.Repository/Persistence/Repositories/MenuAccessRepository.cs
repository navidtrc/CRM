using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class MenuAccessRepository : Repository<Entities.DataModels.Security.MenuAccess>, IMenuAccessRepository
    {
        public MenuAccessRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
