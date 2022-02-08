using CRM.Domain.Models.Ticket.TicketTypeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class TicketTypeBaseModelConfiguration : IEntityTypeConfiguration<TicketTypeBaseModel>
    {
        public void Configure(EntityTypeBuilder<TicketTypeBaseModel> builder)
        {
            builder.ToTable("TicketTypeBaseModel", "TicketTypeModels");
        }
    }
}
