using CRM.Domain.Models.Core;

namespace CRM.Domain.Models.Security
{
    public class AccessRole : BaseEntity
    {
        public Access Access { get; set; }
        public int AccessId { get; set; }

        public Role Role { get; set; }
        public string RoleId { get; set; }
    }
}
