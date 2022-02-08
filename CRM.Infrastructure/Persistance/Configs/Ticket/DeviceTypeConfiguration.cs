using CRM.Domain.Models.Ticket.TicketTypeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Ticket
{
    public class DeviceTypeConfiguration : IEntityTypeConfiguration<DeviceType>
    {
        public void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            builder.ToTable("DeviceType", "TicketTypeModels");
            
            builder.Property(p => p.Title).IsRequired();
        }
    }
}
