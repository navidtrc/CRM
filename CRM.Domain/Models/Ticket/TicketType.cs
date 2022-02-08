using CRM.Domain.Models.Core;
using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket
{
    public class TicketType : BaseEntity
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string ModelId { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
