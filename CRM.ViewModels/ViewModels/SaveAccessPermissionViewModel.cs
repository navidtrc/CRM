using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.ViewModels.ViewModels
{
    public class SaveAccessPermissionViewModel
    {
        public int PermissionId { get; set; }
        public List<int> Accesses { get; set; }
    }
}
