using CRM.Domain.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Security
{
    public class AccessPermissionConfiguration : IEntityTypeConfiguration<AccessPermission>
    {
        public void Configure(EntityTypeBuilder<AccessPermission> builder)
        {
            builder.ToTable("AccessPermission", "Security");
            builder.Property(p => p.Id).UseHiLo($"Sequence-{nameof(AccessPermission)}");

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
