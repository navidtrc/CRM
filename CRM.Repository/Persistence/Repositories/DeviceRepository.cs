using CRM.DAL;
using CRM.Repository.Core.Repositories;
using CRM.Entities.DataModels.Basic;

namespace CRM.Repository.Persistence.Repositories
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
