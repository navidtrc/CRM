using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Domain.Models.Ticket.TicketTypeModels.Invoice>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Ticket.TicketTypeModels.Invoice> builder)
        {
            builder.ToTable("Invoice", "TicketTypeModels");

            builder.HasOne(o => o.Customer)
                .WithMany(m => m.Invoices)
                .HasForeignKey(f => f.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
