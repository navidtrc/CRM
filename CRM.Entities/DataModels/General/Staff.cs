using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using CRM.Entities.DataModels.Security;
using CRM.Entities.DataModels.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entities.DataModels.General
{
    public class Staff : BaseEntity
    {       
        [Display(Name = "StaffCode", ResourceType = typeof(Resource))]
        public int StaffCode { get; set; }

        public Person Person { get; set; }
        public ICollection<Invoice> RepairerInvoices { get; set; }
    }
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff", "General");
        }
    }
}
