using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class TicketFlowConfiguration : IEntityTypeConfiguration<Domain.Models.Ticket.TicketFlow>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Ticket.TicketFlow> builder)
        {
            builder.ToTable("TicketFlow", "Ticket");

            builder.HasOne(o => o.Ticket)
                .WithMany(m => m.TicketFlows)
                .HasForeignKey(f => f.TicketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.ToUser)
                .WithMany(m => m.TicketFlows)
                .HasForeignKey(f => f.ToUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
