using CRM.Domain.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Security
{
    public class UserAccessConfiguration : IEntityTypeConfiguration<UserAccess>
    {
        public void Configure(EntityTypeBuilder<UserAccess> builder)
        {
            builder.ToTable("UserAccess", "Security");
            builder.Property(p => p.Id).UseHiLo($"Sequence-{nameof(UserAccess)}");

            builder.HasOne(o => o.User)
                .WithMany(m => m.UserAccesses)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Access)
                .WithMany(m => m.UserAccesses)
                .HasForeignKey(f => f.AccessId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
