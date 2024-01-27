using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Basic
{
    public class TicketFellow : BaseEntity
    {
        [Display(Name = "Ticket", ResourceType = typeof(Resource))]
        public Ticket Ticket { get; set; }
        
        [Display(Name = "TicketId", ResourceType = typeof(Resource))]
        public long TicketId { get; set; }
        
        [Display(Name = "FellowDate", ResourceType = typeof(Resource))]
        public DateTime FellowDate { get; set; }
       
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public eTicketStatus Status { get; set; }
    }
    public class FellowConfiguration : IEntityTypeConfiguration<TicketFellow>
    {
        public void Configure(EntityTypeBuilder<TicketFellow> builder)
        {
            builder.ToTable("TicketFellow", "Basic");
        }
    }
}