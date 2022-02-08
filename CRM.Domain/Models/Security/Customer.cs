using CRM.Domain.Models.Ticket.TicketTypeModels;
using System.Collections.Generic;

namespace CRM.Domain.Models.Security
{
    public class Customer : Person
    {
        public string CustomerCode { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
