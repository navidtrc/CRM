using CRM.Domain.Common.Enums;
using System.Collections.Generic;

namespace CRM.Application.TicketApplication.Models
{
    public class InvoicePostViewModel
    {
        public string number { get; set; }
        public string date { get; set; }
        public int state { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public List<DevicePostViewModel> devices { get; set; }
    }
   
    public class DevicePostViewModel
    {
        public string state { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string accessories { get; set; }
        public string description { get; set; }
        public long? customerPrice { get; set; }
        public long? shopPrice { get; set; }
        public bool repairWarranty { get; set; }
        public bool shopWarranty { get; set; }
        //public InquiryPostViewModel inquiry { get; set; }
    }
    public class InquiryPostViewModel
    {

        public InquiryCallPostViewModel[] inquiries { get; set; }
    }
    public class InquiryCallPostViewModel
    {

    }

}
