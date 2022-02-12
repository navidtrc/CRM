using Common.Enums;
using CRM.Domain.Models.Core;
using CRM.Domain.Models.Ticket;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CRM.Domain.Models.Security
{
    public class User : IdentityUser, IEntity<string>
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Person Person { get; set; }
        public eUserFrom RegisteredType { get; set; } = eUserFrom.Internal;
        public DateTimeOffset? LastLoginDate { get; set; }
        public string ConfirmationCodeHash { get; set; }

        //public ICollection<UserPermission> UserPermissions { get; set; }
        //public ICollection<UserAccess> UserAccesses { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
