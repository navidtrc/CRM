using CRM.Domain.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Security
{
    public class AccessRoleConfiguration : IEntityTypeConfiguration<AccessRole>
    {
        public void Configure(EntityTypeBuilder<AccessRole> builder)
        {
            builder.ToTable("AccessRole", "Security");
            builder.Property(p => p.Id).UseHiLo($"Sequence-{nameof(AccessRole)}");

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
