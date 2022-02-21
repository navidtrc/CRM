using CRM.Domain.Models.Core;
using System;

namespace CRM.Domain.Models.Ticket
{
    public class InquiryCall : BaseEntity
    {
        public DateTime CallDateTime { get; set; }
        public bool? IsAnswered { get; set; }
        public bool? IsConfirmed { get; set; }
        public int InquiryId { get; set; }
        public Inquiry Inquiry { get; set; }
    }
}
