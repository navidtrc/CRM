using Newtonsoft.Json;
using System.Collections.Generic;

namespace CRM.ViewModels.ViewModels
{

    [JsonObject(MemberSerialization.OptIn)]
    public class TicketPrerequisiteViewModel
    {
        [JsonProperty(PropertyName = "LastTicketNumber")]
        public int LastTicketNumber { get; set; }

        [JsonProperty(PropertyName = "DeviceTypeList")]
        public List<DeviceTypeViewModel> DeviceTypeList { get; set; }

        [JsonProperty(PropertyName = "DeviceBrandList")]
        public List<DeviceBrandViewModel> DeviceBrandList { get; set; }

    }

    [JsonObject(MemberSerialization.OptIn)]
    public class DeviceTypeViewModel
    {

        [JsonProperty(PropertyName = "id")]
        public long id { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string label { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class DeviceBrandViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public long id { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string label { get; set; }
    }
}
