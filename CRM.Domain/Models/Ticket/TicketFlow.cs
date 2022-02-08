using CRM.Domain.Models.Core;
using CRM.Domain.Models.Security;
using System;

namespace CRM.Domain.Models.Ticket
{
    public class TicketFlow : BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string ToUserId { get; set; }
        public User ToUser { get; set; }
    }
}
