using Newtonsoft.Json;

namespace CRM.ViewModels.ViewModels
{

    [JsonObject(MemberSerialization.OptIn)]
    public class TicketAddEditViewModel
    {
        [JsonProperty(PropertyName = "UserId")]
        public long TicketId { get; set; }


        [JsonProperty(PropertyName = "TicketNumber")]
        public int TicketNumber { get; set; }


        [JsonProperty(PropertyName = "TicketDate")]
        public string TicketDate { get; set; }


        [JsonProperty(PropertyName = "CustomerName")]
        public string CustomerName { get; set; }


        [JsonProperty(PropertyName = "CustomerPhone")]
        public string CustomerPhone { get; set; }


        [JsonProperty(PropertyName = "CustomerEmail")]
        public string CustomerEmail { get; set; }


        [JsonProperty(PropertyName = "DeviceTypeId")]
        public long DeviceTypeId { get; set; }


        [JsonProperty(PropertyName = "DeviceBrandId")]
        public long DeviceBrandId { get; set; }


        [JsonProperty(PropertyName = "DeviceModel")]
        public string DeviceModel { get; set; }


        [JsonProperty(PropertyName = "DeviceDescrption")]
        public string DeviceDescrption { get; set; }


        [JsonProperty(PropertyName = "DeviceAccessories")]
        public string DeviceAccessories { get; set; }


        [JsonProperty(PropertyName = "DeviceWaranty")]
        public bool DeviceWaranty { get; set; }


        [JsonProperty(PropertyName = "InquiryPrice")]
        public int InquiryPrice { get; set; }
    }
}
