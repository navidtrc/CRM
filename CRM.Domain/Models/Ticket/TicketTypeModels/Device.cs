using System.Collections.Generic;

namespace CRM.Domain.Models.Ticket.TicketTypeModels
{
    public class Device : TicketTypeBaseModel
    {
        public readonly static string ModelId = "2";
        public readonly static string ModelName = nameof(Device);

        public string Brand { get; set; }
        public string Model { get; set; }
        public string Accessories { get; set; }
        public bool ShopWarranty { get; set; }
        public bool RepairWarranty { get; set; }
        public string WarrantyDesc { get; set; }
        public decimal? CustomerPrice { get; set; }
        public decimal? ShopPrice { get; set; }

        #region NavProps
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        public ICollection<Inquiry> Inquiries { get; set; }
        #endregion
    }
}
