using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.HelperModels
{
    public class AccessCache
    {
        public string route { get; set; }
        public bool allowanonymous { get; set; }
        public Guid userid { get; set; }
    }
}
