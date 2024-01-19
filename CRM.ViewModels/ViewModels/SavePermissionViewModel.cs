using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.ViewModels.ViewModels
{
    public class SavePermissionViewModel
    {
        public Guid UserId { get; set; }
        public string AccessType { get; set; }
        public List<object> Permissions { get; set; }
    }
}
