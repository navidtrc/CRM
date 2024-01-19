using CRM.Common.Enums;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.DataModels.General
{
    public class ContactLabel : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
    public class ContactLabelConfiguration : IEntityTypeConfiguration<ContactLabel>
    {
        public void Configure(EntityTypeBuilder<ContactLabel> builder)
        {
            builder.ToTable("ContactLabel", "General");
        }
    }
}
