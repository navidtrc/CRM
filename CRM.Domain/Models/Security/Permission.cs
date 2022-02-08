using CRM.Domain.Models.Core;
using System.Collections.Generic;

namespace CRM.Domain.Models.Security
{
    public class Permission : BaseEntity
    {
        public string Title { get; set; }
        public int OrderIndex { get; set; }
        public Permission ParantPermission { get; set; }
        public int? ParentId { get; set; }

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<AccessPermission> AccessPermissions { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}
