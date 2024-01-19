using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.ViewModels.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserLockoutViewModel
    {
        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }
        
        [JsonProperty(PropertyName = "LockoutEnabled")]
        public bool LockoutEnabled { get; set; }
        
        [JsonProperty(PropertyName = "LockoutEnd")]
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
