using CRM.Domain.Models.Ticket;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class InquiryCallRepository : Repository<InquiryCall>, IInquiryCallRepository
    {
        public InquiryCallRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
