using System;

namespace CRM.Domain.Models.Ticket.TicketTypeModels
{
    public class InquiryDate : TicketTypeBaseModel
    {
        public readonly static string ModelId = "5";
        public readonly static string ModelName = nameof(InquiryDate);
        
        public DateTime CallDateTime { get; set; }
        public bool IsAnswered { get; set; }
        public int InquiryId { get; set; }
        public Inquiry Inquiry { get; set; }
    }
}
