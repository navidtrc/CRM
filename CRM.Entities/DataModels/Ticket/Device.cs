using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.DataModels.Ticket
{
    public class Device : BaseEntity
    {
        [Display(Name = "Brand", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Brand { get; set; }

        [Display(Name = "Model", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Model { get; set; }

        [Display(Name = "Accessories", ResourceType = typeof(Resource))]
        public string Accessories { get; set; }

        [Display(Name = "ShopWarranty", ResourceType = typeof(Resource))]
        public bool ShopWarranty { get; set; }

        [Display(Name = "RepairWarranty", ResourceType = typeof(Resource))]
        public bool RepairWarranty { get; set; }

        [Display(Name = "WarrantyDesc", ResourceType = typeof(Resource))]
        public string WarrantyDesc { get; set; }

        [Display(Name = "CustomerPrice", ResourceType = typeof(Resource))]
        public decimal? CustomerPrice { get; set; }

        [Display(Name = "ShopPrice", ResourceType = typeof(Resource))]
        public decimal? ShopPrice { get; set; }

        #region NavProps
        [Display(Name = "FactorId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long FactorId { get; set; }

        [Display(Name = "Factor", ResourceType = typeof(Resource))]
        public Invoice Factor { get; set; }

        [Display(Name = "DeviceKindId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long KindId { get; set; }

        [Display(Name = "DeviceKind", ResourceType = typeof(Resource))]
        public DeviceKind DeviceKind { get; set; }

        public ICollection<InvoiceFellow> Fellows { get; set; }
        public ICollection<Inquiry> Inquiries { get; set; }
        #endregion
    }
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Device", "Ticket");
            builder.HasOne(o => o.DeviceKind).WithMany(m => m.Devices).HasForeignKey(f => f.KindId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Factor).WithMany(m => m.Devices).HasForeignKey(f => f.FactorId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(o => o.Fellows).WithOne(m => m.Device).HasForeignKey(f => f.DeviceId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(o => o.Inquiries).WithOne(m => m.Device).HasForeignKey(f => f.DeviceId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
