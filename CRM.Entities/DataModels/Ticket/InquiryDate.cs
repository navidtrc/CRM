using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.DataModels.Ticket
{
    public class InquiryDate : BaseEntity
    {
        [Display(Name = "CallDate", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public DateTime CallDate { get; set; }
       
        [Display(Name = "IsAnswered", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public bool IsAnswered { get; set; }

        [Display(Name = "InquiryId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long InquiryId { get; set; }

        [Display(Name = "Inquiry", ResourceType = typeof(Resource))]
        public virtual Inquiry Inquiry { get; set; }
    }
    public class InquiryDateConfiguration : IEntityTypeConfiguration<InquiryDate>
    {
        public void Configure(EntityTypeBuilder<InquiryDate> builder)
        {
            builder.ToTable("InquiryDate", "Ticket");
            builder.HasOne(o => o.Inquiry).WithMany(m => m.InquiryDates).HasForeignKey(f => f.InquiryId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
