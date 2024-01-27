using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Basic
{
    public class Device : BaseEntity
    {
        [Display(Name = "DeviceKindId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long DeviceKindId { get; set; }

        [Display(Name = "DeviceKind", ResourceType = typeof(Resource))]
        public DeviceKind DeviceKind { get; set; }

        [Display(Name = "Brand", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Brand { get; set; }

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
            builder.HasOne(o => o.DeviceKind).WithMany(m => m.Devices).HasForeignKey(f => f.DeviceKindId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
