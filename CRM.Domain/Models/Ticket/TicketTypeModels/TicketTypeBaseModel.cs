using CRM.Domain.Models.Core;
using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket.TicketTypeModels
{
    public class TicketTypeBaseModel : BaseEntity
    {
        public ICollection<Ticket> Tickets  { get; set; }
    }
}
