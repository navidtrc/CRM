using CRM.Domain.Models.Ticket.TicketTypeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Device", "TicketTypeModels");

            builder.HasOne(o => o.Invoice)
                .WithMany(m => m.Devices)
                .HasForeignKey(f => f.InvoiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.DeviceType)
                .WithMany(m => m.Devices)
                .HasForeignKey(f => f.DeviceTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Brand).IsRequired();
            builder.Property(p => p.Model).IsRequired();
        }
    }
}
