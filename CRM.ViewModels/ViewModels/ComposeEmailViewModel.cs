using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRM.ViewModels.ViewModels
{
    public class ComposeEmailViewModel
    {
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
