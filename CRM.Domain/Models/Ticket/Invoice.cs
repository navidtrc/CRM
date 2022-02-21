using CRM.Domain.Common.Enums;
using CRM.Domain.Models.Core;
using CRM.Domain.Models.Security;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CRM.Domain.Models.Ticket
{
    public class Invoice : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int StateId { get; set; }
        public Lookup State { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Device> Devices { get; set; }
        public object this[string propertyName]
        {
            get
            {
                PropertyInfo property = GetType().GetProperty(propertyName);
                return property.GetValue(this, null);
            }
            set
            {
                PropertyInfo property = GetType().GetProperty(propertyName);
                property.SetValue(this, value, null);
            }
        }

    }
}
