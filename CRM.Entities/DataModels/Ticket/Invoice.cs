using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using CRM.Entities.DataModels.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.DataModels.Ticket
{
    public class Invoice : BaseEntity
    {
        [Display(Name = "FactorDate", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public DateTime FactorDate { get; set; }

        [Display(Name = "FactorNumber", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public int FactorNumber { get; set; }

        [Display(Name = "DoneDate", ResourceType = typeof(Resource))]
        public DateTime? DoneDate { get; set; }

        #region NavProps
        [Display(Name = "Repairer", ResourceType = typeof(Resource))]
        public Staff Repairer { get; set; }
        [Display(Name = "RepairerId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long RepairerId { get; set; }
       
        [Display(Name = "InvoiceStatus", ResourceType = typeof(Resource))]
        public eInvoiceStatus Status { get; set; }

        public ICollection<Device> Devices { get; set; }
        #endregion
    }
    public class StatusConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice", "Ticket");
            builder.HasOne(o => o.Repairer).WithMany(m => m.RepairerInvoices).HasForeignKey(f => f.RepairerId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
