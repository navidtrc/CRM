using CRM.Domain.Models.Ticket.TicketTypeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class InquiryDateConfiguration : IEntityTypeConfiguration<InquiryDate>
    {
        public void Configure(EntityTypeBuilder<InquiryDate> builder)
        {
            builder.ToTable("InquiryDate", "TicketTypeModels");

            builder.HasOne(o => o.Inquiry)
                .WithMany(m => m.InquiryDates)
                .HasForeignKey(f => f.InquiryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
