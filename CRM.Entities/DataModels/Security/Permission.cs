using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace CRM.Entities.DataModels.Security
{
    public class Permission : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<AccessPermission> AccessPermissions { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", "Security");
            builder.Property(p => p.Title).IsRequired(true);
        }
    }
}
