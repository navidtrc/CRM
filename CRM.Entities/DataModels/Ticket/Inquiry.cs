using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Ticket
{
    public class Inquiry : BaseEntity
    {
        [Display(Name = "Reason", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Reason { get; set; }
       
        [Display(Name = "Price", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public decimal Price { get; set; }
        
        [Display(Name = "IsConfirmed", ResourceType = typeof(Resource))]
        public bool? IsConfirmed { get; set; }

        #region NavProps
        [Display(Name = "DeviceId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long DeviceId { get; set; }
       
        [Display(Name = "Device", ResourceType = typeof(Resource))]
        public virtual Device Device { get; set; }
        
        public virtual ICollection<InquiryDate> InquiryDates { get; set; }
        #endregion
    }
    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.ToTable("Inquiry", "Ticket");
        }
    }
}
