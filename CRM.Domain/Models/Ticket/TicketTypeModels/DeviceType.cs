using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket.TicketTypeModels
{
    public class DeviceType : TicketTypeBaseModel
    {
        public readonly static string ModelId = "3";
        public readonly static string ModelName = nameof(DeviceType);

        public string Title { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
