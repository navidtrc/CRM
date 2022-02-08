using CRM.Domain.Models.Ticket.TicketTypeModels;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class DeviceTypeRepository : Repository<DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
