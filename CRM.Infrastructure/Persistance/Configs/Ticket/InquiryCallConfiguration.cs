using CRM.Domain.Models.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class InquiryCallConfiguration : IEntityTypeConfiguration<InquiryCall>
    {
        public void Configure(EntityTypeBuilder<InquiryCall> builder)
        {
            builder.ToTable("InquiryCall", "Ticket");

            builder.HasOne(o => o.Inquiry)
                .WithMany(m => m.InquiryDates)
                .HasForeignKey(f => f.InquiryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
