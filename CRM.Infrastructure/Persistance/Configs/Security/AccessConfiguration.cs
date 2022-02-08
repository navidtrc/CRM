using CRM.Domain.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Security
{
    public class AccessConfiguration : IEntityTypeConfiguration<Access>
    {
        public void Configure(EntityTypeBuilder<Access> builder)
        {
            builder.ToTable("Access", "Security");
            builder.Property(p => p.Id).UseHiLo($"Sequence-{nameof(Access)}");

            builder.HasIndex(i => i.Code).IsUnique(true);
            builder.Property(p => p.ControllerName).IsRequired(true);
            builder.Property(p => p.ActionName).IsRequired(true);
            builder.Property(p => p.Route).IsRequired(true);
        }
    }
}
