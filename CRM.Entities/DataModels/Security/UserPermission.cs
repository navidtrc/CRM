using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.DataModels.Security
{
    public class UserPermission : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }

        public Permission Permission { get; set; }
        public long PermissionId { get; set; }
        public ICollection<AccessPermission> AccessPermissions { get; set; }
    }
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermission", "Security");

            builder.HasOne(o => o.User)
               .WithMany(m => m.UserPermissions)
               .HasForeignKey(f => f.UserId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Permission)
               .WithMany(m => m.UserPermissions)
               .HasForeignKey(f => f.PermissionId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
