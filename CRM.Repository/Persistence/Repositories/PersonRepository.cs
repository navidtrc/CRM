using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class PersonRepository : Repository<Entities.DataModels.Security.Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
