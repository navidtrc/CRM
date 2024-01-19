using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.ViewModels.ViewModels
{
    public class UserInfoViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
