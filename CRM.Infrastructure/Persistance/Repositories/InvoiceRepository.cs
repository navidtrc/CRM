using CRM.Domain.Models.Ticket;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
