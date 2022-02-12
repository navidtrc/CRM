using CRM.Domain.Models.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice", "Ticket");

            builder.HasOne(o => o.Customer)
                .WithMany(m => m.Invoices)
                .HasForeignKey(f => f.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
