using Common;
using CRM.Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace CRM.Application.TicketApplication.Commands.Create
{
    public class InvoiceCreateCommand : IRequest<OperationResult>
    {
        public string number { get; set; }
        public DateTime date { get; set; }
        public eInvoiceState state { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public List<DeviceVM> devices { get; set; }
    }
    public class DeviceVM
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
}
