using CRM.Domain.Models.Core;
using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket
{
    public class Inquiry : BaseEntity
    {
        public string Reason { get; set; }
        public long Price { get; set; } = 0;
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public ICollection<InquiryCall> InquiryDates { get; set; }
    }
}
