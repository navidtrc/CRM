using CRM.Domain.Models.Ticket.TicketTypeModels;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
