using CRM.Domain.Models.Ticket.TicketTypeModels;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class InquiryDateRepository : Repository<InquiryDate>, IInquiryDateRepository
    {
        public InquiryDateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
