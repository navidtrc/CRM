using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Basic
{
    public class DeviceType : BaseEntity
    {
        [Display(Name = "Title", ResourceType = typeof(Resource))]
        public string Title { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
    public class DeviceTypeConfiguration : IEntityTypeConfiguration<DeviceType>
    {
        public void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            builder.ToTable("DeviceType", "Basic");
        }
    }
}
