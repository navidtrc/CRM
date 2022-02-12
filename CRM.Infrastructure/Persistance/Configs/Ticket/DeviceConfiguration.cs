using CRM.Domain.Models.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Device", "Ticket");

            builder.HasOne(o => o.Invoice)
                .WithMany(m => m.Devices)
                .HasForeignKey(f => f.InvoiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Brand).IsRequired();
            builder.Property(p => p.Model).IsRequired();
        }
    }
}
