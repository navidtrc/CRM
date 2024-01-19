using CRM.Common.Resources.StringResources;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.ViewModels.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ForgetPasswordConfirmViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        [Display(Name = "Id", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Password")]
        [Display(Name = "Password", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "ConfirmPassword")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }
    }
}
