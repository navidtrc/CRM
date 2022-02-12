using CRM.Domain.Models.Core;

namespace CRM.Domain.Models.Ticket
{
    public class Device : BaseEntity
    {
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Accessories { get; set; }
        public bool ShopWarranty { get; set; }
        public bool RepairWarranty { get; set; }
        public long? CustomerPrice { get; set; }
        public long? ShopPrice { get; set; }

        #region NavProps
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public Inquiry Inquiry { get; set; }
        #endregion
    }
}
