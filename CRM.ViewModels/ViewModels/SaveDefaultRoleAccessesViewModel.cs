using System;
using System.Collections.Generic;

namespace CRM.ViewModels.ViewModels
{
    public class SaveAccessRoleViewModel
    {
        public Guid RoleId { get; set; }
        public List<int> Accesses { get; set; }
    }
}
