using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.ViewModels.ViewModels
{
    public class SavePermissionReverseViewModel
    {
        public object PermissionId { get; set; }
        public string AccessType { get; set; }
        public List<Guid> Users { get; set; }
    }
}
