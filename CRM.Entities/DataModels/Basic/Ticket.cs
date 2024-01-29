using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Basic
{
    public class Ticket : BaseEntity
    {
        [Display(Name = "Number", ResourceType = typeof(Resource))]
        public int Number { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Resource))]
        public DateTime Date { get; set; }

        [Display(Name = "CloseDate", ResourceType = typeof(Resource))]
        public DateTime? CloseDate { get; set; }
        
        [Display(Name = "Customer", ResourceType = typeof(Resource))]
        public Customer Customer { get; set; }

        [Display(Name = "CustomerId", ResourceType = typeof(Resource))]
        public long CustomerId { get; set; }

        [Display(Name = "Repairer", ResourceType = typeof(Resource))]
        public Staff Repairer { get; set; }

        [Display(Name = "RepairerId", ResourceType = typeof(Resource))]
        public long? RepairerId { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public eTicketStatus LastStatus { get; set; } = eTicketStatus.Waiting;

        [Display(Name = "InquiryPrice", ResourceType = typeof(Resource))]
        public int InquiryPrice { get; set; }

        [Display(Name = "RepairPrice", ResourceType = typeof(Resource))]
        public int RepairPrice { get; set; }

        [Display(Name = "FinalPrice", ResourceType = typeof(Resource))]
        public int FinalPrice { get; set; }

        [Display(Name = "ProfitMargin", ResourceType = typeof(Resource))]
        public int ProfitMargin { get; set; }

        [Display(Name = "Device", ResourceType = typeof(Resource))]
        public Device Device { get; set; }

        [Display(Name = "DeviceId", ResourceType = typeof(Resource))]
        public long DeviceId { get; set; }

        [Display(Name = "Fellows", ResourceType = typeof(Resource))]
        public ICollection<TicketFellow> Fellows { get; set; }

        [Display(Name = "Fellows", ResourceType = typeof(Resource))]
        public Inquiry Inquiry { get; set; }
    }
    public class StatusConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket", "Basic");
            builder.HasOne(o => o.Repairer).WithMany(m => m.RepairerTickets).HasForeignKey(f => f.RepairerId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Customer).WithMany(m => m.Tickets).HasForeignKey(f => f.CustomerId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Device).WithOne(m => m.Ticket).HasForeignKey<Ticket>(f => f.DeviceId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.Fellows).WithOne(o => o.Ticket).HasForeignKey(f => f.TicketId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Inquiry).WithOne(m => m.Ticket).HasForeignKey<Inquiry>(f => f.TicketId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
