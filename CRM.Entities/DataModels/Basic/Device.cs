using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Basic
{
    public class Device : BaseEntity
    {
        [Display(Name = "DeviceTypeId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long DeviceTypeId { get; set; }

        [Display(Name = "DeviceType", ResourceType = typeof(Resource))]
        public DeviceType DeviceType { get; set; }

        [Display(Name = "DeviceBrandId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long DeviceBrandId { get; set; }

        [Display(Name = "DeviceType", ResourceType = typeof(Resource))]
        public DeviceBrand DeviceBrand { get; set; }

        [Display(Name = "Model", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Model { get; set; }

        [Display(Name = "Accessories", ResourceType = typeof(Resource))]
        public string Accessories { get; set; }

        [Display(Name = "Warranty", ResourceType = typeof(Resource))]
        public bool Warranty { get; set; }

        public Ticket Ticket { get; set; }
    }
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Device", "Basic");
            builder.HasOne(o => o.DeviceType).WithMany(m => m.Devices).HasForeignKey(f => f.DeviceTypeId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.DeviceBrand).WithMany(m => m.Devices).HasForeignKey(f => f.DeviceBrandId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
