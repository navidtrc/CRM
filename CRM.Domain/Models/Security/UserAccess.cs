using CRM.Domain.Models.Core;

namespace CRM.Domain.Models.Security
{
    public class UserAccess : BaseEntity 
    {
        public User User { get; set; }
        public string UserId { get; set; }

        public Access Access { get; set; }
        public int AccessId { get; set; }
    }
}
