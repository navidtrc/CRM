using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CRM.Entities.DataModels.Security
{
    public class AccessRole : BaseEntity
    {
        public Access Access { get; set; }
        public long AccessId { get; set; }

        public Role Role { get; set; }
        public Guid RoleId { get; set; }
    }
    public class DefaultRoleAccessConfiguration : IEntityTypeConfiguration<AccessRole>
    {
        public void Configure(EntityTypeBuilder<AccessRole> builder)
        {
            builder.ToTable("AccessRole", "Security");

            builder.HasOne(o => o.Access)
                .WithMany(m => m.AccessRoles)
                .HasForeignKey(f => f.AccessId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(o => o.Role)
                .WithMany(m => m.AccessRoles)
                .HasForeignKey(f => f.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
