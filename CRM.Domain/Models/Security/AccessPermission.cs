using CRM.Domain.Models.Core;

namespace CRM.Domain.Models.Security
{
    public class AccessPermission : BaseEntity
    {
        public Access Access { get; set; }
        public int AccessId { get; set; }
        public Permission Permission { get; set; }
        public int PermissionId { get; set; }
        
    }
}
