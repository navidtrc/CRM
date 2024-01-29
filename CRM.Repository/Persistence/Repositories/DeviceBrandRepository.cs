using CRM.DAL;
using CRM.Repository.Core.Repositories;
using CRM.Entities.DataModels.Basic;

namespace CRM.Repository.Persistence.Repositories
{
    public class DeviceBrandRepository : Repository<DeviceBrand>, IDeviceBrandRepository
    {
        public DeviceBrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
