using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class CustomerRepository : Repository<Entities.DataModels.General.Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
