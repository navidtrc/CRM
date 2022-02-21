using CRM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs
{
    public class LookupConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.ToTable("Lookup","dbo");
            builder.HasIndex(p => new { p.TypeTitle, p.TypeId }).IsUnique();
            builder.Property(p => p.Aux1).IsRequired();

            builder.HasMany(o => o.Device_Brands)
                .WithOne(m => m.DeviceBrand)
                .HasForeignKey(f => f.DeviceBrandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.Device_Types)
                .WithOne(m => m.DeviceType)
                .HasForeignKey(f => f.DeviceTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.Invoice_States)
                .WithOne(m => m.State)
                .HasForeignKey(f => f.StateId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
