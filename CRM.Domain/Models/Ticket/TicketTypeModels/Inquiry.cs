using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket.TicketTypeModels
{
    public class Inquiry : TicketTypeBaseModel
    {
        public readonly static string ModelId = "4";
        public readonly static string ModelName = nameof(Inquiry);

        public string Reason { get; set; }
        public decimal Price { get; set; }
        public bool? IsConfirmed { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public ICollection<InquiryDate> InquiryDates { get; set; }
    }
}
