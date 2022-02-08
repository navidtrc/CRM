using CRM.Domain.Models.Ticket.TicketTypeModels;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class InquiryRepository : Repository<Inquiry>, IInquiryRepository
    {
        public InquiryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
