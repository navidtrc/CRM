using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using CRM.Entities.DataModels.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Basic
{
    public class Staff : BaseEntity
    {       
        [Display(Name = "StaffCode", ResourceType = typeof(Resource))]
        public int StaffCode { get; set; }

        public Person Person { get; set; }
        public ICollection<Ticket> RepairerTickets { get; set; }
    }
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff", "Basic");
        }
    }
}
