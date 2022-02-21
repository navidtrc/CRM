using CRM.Domain.Models.Core;
using CRM.Domain.Models.Ticket;
using System.Collections.Generic;

namespace CRM.Domain.Models
{
    public class Lookup : BaseEntity
    {
        public string TypeTitle { get; set; }
        public int TypeId { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }

        public ICollection<Device> Device_Types { get; set; }
        public ICollection<Device> Device_Brands { get; set; }
        public ICollection<Invoice> Invoice_States { get; set; }
    }
}
