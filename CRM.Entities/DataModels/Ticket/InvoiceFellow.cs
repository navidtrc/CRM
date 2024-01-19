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
    public class InvoiceFellow : BaseEntity
    {
        [Display(Name = "SendDate", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public DateTime SendDate { get; set; }
        
        [Display(Name = "BackDate", ResourceType = typeof(Resource))]
        public DateTime? BackDate { get; set; }

        #region NavProps
        [Display(Name = "DeviceId", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long DeviceId { get; set; }
        
        [Display(Name = "Device", ResourceType = typeof(Resource))]
        public virtual Device Device { get; set; }
        #endregion
    }
    public class FellowConfiguration : IEntityTypeConfiguration<InvoiceFellow>
    {
        public void Configure(EntityTypeBuilder<InvoiceFellow> builder)
        {
            builder.ToTable("InvoiceFellow", "Ticket");
        }
    }
}