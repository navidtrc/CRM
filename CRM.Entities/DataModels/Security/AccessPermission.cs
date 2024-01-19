using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.DataModels.Security
{
    public class AccessPermission : BaseEntity
    {
        public Access Access { get; set; }
        public long AccessId { get; set; }
        
        public Permission Permission { get; set; }
        public long PermissionId { get; set; }
        
    }
    public class AccessPermissionGroupConfiguration : IEntityTypeConfiguration<AccessPermission>
    {
        public void Configure(EntityTypeBuilder<AccessPermission> builder)
        {
            builder.ToTable("AccessPermission", "Security");

            builder.HasOne(o => o.Access)
                .WithMany(m => m.AccessPermission)
                .HasForeignKey(f => f.AccessId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Permission)
                .WithMany(m => m.AccessPermissions)
                .HasForeignKey(f => f.PermissionId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
