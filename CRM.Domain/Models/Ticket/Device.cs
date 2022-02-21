using CRM.Domain.Models.Core;

namespace CRM.Domain.Models.Ticket
{
    public class Device : BaseEntity
    {
        public int DeviceTypeId { get; set; }
        public Lookup DeviceType { get; set; }

        public int DeviceBrandId { get; set; }
        public Lookup DeviceBrand { get; set; }

        public string Model { get; set; }
        public string Accessories { get; set; }
        public bool ShopWarranty { get; set; }
        public bool RepairWarranty { get; set; }
        public long CustomerPrice { get; set; } = 0;
        public long ShopPrice { get; set; } = 0;

        #region NavProps
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int InquiryId { get; set; }
        public Inquiry Inquiry { get; set; }
        #endregion
    }
}
