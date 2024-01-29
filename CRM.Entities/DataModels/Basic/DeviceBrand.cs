using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Basic
{
    public class DeviceBrand : BaseEntity
    {
        [Display(Name = "Title", ResourceType = typeof(Resource))]
        public string Title { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
    public class DeviceBrandConfiguration : IEntityTypeConfiguration<DeviceBrand>
    {
        public void Configure(EntityTypeBuilder<DeviceBrand> builder)
        {
            builder.ToTable("DeviceBrand", "Basic");
        }
    }
}
