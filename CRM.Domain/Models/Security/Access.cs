using CRM.Domain.Common.Enums;
using CRM.Domain.Models.Core;
using System.Collections.Generic;

namespace CRM.Domain.Models.Security
{
    public class Access : BaseEntity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public eAccessType AccessType { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Route { get; set; }
        public int OrderIndex { get; set; }
        public bool AllowAnonymous { get; set; } = false;
        
        public ICollection<AccessPermission> AccessPermission { get; set; }
        public ICollection<AccessRole> AccessRoles { get; set; }
        public ICollection<UserAccess> UserAccesses { get; set; }
    }
}
