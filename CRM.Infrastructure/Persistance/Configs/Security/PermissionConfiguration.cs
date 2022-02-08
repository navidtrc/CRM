using CRM.Domain.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Security
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", "Security");
            builder.Property(p => p.Id).UseHiLo($"Sequence-{nameof(Permission)}");

            builder.HasOne(o => o.ParantPermission)
                .WithMany(o => o.Permissions)
                .HasForeignKey(f => f.ParentId)
                .IsRequired(false);
        }
    }
}
