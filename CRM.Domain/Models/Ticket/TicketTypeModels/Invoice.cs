using CRM.Domain.Common.Enums;
using CRM.Domain.Models.Security;
using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket.TicketTypeModels
{
    public class Invoice : TicketTypeBaseModel
    {
        public readonly static string ModelId = "1";
        public readonly static string ModelName = nameof(Invoice);

        public string Number { get; set; }
        public eInvoiceState State { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
