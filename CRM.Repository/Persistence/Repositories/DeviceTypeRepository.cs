using CRM.DAL;
using CRM.Repository.Core.Repositories;
using CRM.Entities.DataModels.Basic;

namespace CRM.Repository.Persistence.Repositories
{
    public class DeviceTypeRepository : Repository<DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
