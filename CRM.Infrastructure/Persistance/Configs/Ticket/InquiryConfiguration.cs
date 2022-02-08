using CRM.Domain.Models.Ticket.TicketTypeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.ToTable("Inquiry", "TicketTypeModels");

            builder.HasOne(o => o.Device)
                .WithMany(m => m.Inquiries)
                .HasForeignKey(f => f.DeviceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Reason).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.IsConfirmed).IsRequired(false);
        }
    }
}
