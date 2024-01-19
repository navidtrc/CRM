using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Ticket
{
    public class DeviceKind : BaseEntity
    {
        [Display(Name = "Title", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Title { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
    public class DeviceKindConfiguration : IEntityTypeConfiguration<DeviceKind>
    {
        public void Configure(EntityTypeBuilder<DeviceKind> builder)
        {
            builder.ToTable("DeviceKind", "Ticket");
        }
    }
}
