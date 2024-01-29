using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CRM.ViewModels.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PersonUser_AddEdit_ViewModel    
    {
        [JsonProperty(PropertyName = "User")]
        public UserViewModel User { get; set; }

        [JsonProperty(PropertyName = "Person")]
        public PersonViewModel Person { get; set; }
    }
    public class UserViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [JsonProperty(PropertyName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
    }
    public class PersonViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "ePersonType")]
        public ePersonType ePersonType { get; set; }

    }
}
