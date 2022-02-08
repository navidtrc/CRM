using CRM.Domain.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Security
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermission", "Security");
            builder.Property(p => p.Id).UseHiLo($"Sequence-{nameof(UserPermission)}");

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
