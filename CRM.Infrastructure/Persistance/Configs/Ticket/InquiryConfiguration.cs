using CRM.Domain.Models.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.ToTable("Inquiry", "Ticket");

            builder.HasOne(o => o.Device)
                .WithOne(m => m.Inquiry)
                .HasForeignKey<Inquiry>(f => f.DeviceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Reason).IsRequired();
            builder.Property(p => p.Price).IsRequired();
        }
    }
}
