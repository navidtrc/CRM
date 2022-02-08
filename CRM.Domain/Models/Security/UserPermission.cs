using CRM.Domain.Models.Core;
using System.Collections.Generic;

namespace CRM.Domain.Models.Security
{
    public class UserPermission : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }

        public Permission Permission { get; set; }
        public int PermissionId { get; set; }
    }
}
