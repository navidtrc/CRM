using CRM.Domain.Models.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
    {
        public void Configure(EntityTypeBuilder<TicketType> builder)
        {
            builder.ToTable("TicketType", "Ticket");

            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.ModelId).IsRequired();
        }
    }
}