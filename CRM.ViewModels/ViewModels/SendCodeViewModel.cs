using CRM.Common;
using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.ViewModels.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SendCodeViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public eForgetPasswordVia ContactType { get; set; }
    }



    [JsonObject(MemberSerialization.OptIn)]
    public class ConfirmCodeViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public eForgetPasswordVia ContactType { get; set; }

        [JsonProperty(PropertyName = "Code")]
        public int Code { get; set; }
    }


}
