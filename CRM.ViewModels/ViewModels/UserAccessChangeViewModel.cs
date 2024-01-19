using Newtonsoft.Json;

namespace CRM.ViewModels.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserAccessChangeViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "LockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        [JsonProperty(PropertyName = "ChangePassword")]
        public bool ChangePassword { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
