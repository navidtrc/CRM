using CRM.Domain.Common.Enums;
using CRM.Domain.Models.Core;
using CRM.Domain.Models.Security;
using System;
using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket
{
    public class Invoice : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public eInvoiceState State { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
