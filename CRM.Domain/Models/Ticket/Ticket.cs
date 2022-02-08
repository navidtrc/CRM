using CRM.Domain.Models.Core;
using CRM.Domain.Models.Security;
using CRM.Domain.Models.Ticket.TicketTypeModels;
using System;
using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket
{
    public class Ticket : BaseEntity
    {
        public string Number { get; set; }
        public string Subject { get; set; }
        public int TicketTypeBaseModelId { get; set; }
        public TicketTypeBaseModel TicketTypeBaseModel { get; set; }
        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int? ParentId { get; set; }
        public Ticket Parent { get; set; }
        public ICollection<Ticket> Childs { get; set; }
        public ICollection<TicketFlow> TicketFlows { get; set; }

    }
}
