using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class TicketConfiguration : IEntityTypeConfiguration<Domain.Models.Ticket.Ticket>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Ticket.Ticket> builder)
        {
            builder.ToTable("Ticket", "Ticket");

            builder.HasOne(o => o.TicketTypeBaseModel)
                .WithMany(m => m.Tickets)
                .HasForeignKey(f => f.TicketTypeBaseModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.TicketType)
                .WithMany(m => m.Tickets)
                .HasForeignKey(f => f.TicketTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.User)
                .WithMany(m => m.Tickets)
                .HasForeignKey(f => f.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Parent)
                .WithMany(m => m.Childs)
                .HasForeignKey(f => f.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
