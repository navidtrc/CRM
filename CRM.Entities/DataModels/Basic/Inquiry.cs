using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Basic
{
    public class Inquiry : BaseEntity
    {
        [Display(Name = "IsConfirmed", ResourceType = typeof(Resource))]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "TicketId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long TicketId { get; set; }

        [Display(Name = "Ticket", ResourceType = typeof(Resource))]
        public Ticket Ticket { get; set; }
    }
    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.ToTable("Inquiry", "Basic");
        }
    }
}
