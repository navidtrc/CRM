using CRM.Common.Enums;
using CRM.Entities.Core;
using CRM.Entities.DataModels.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Entities.DataModels.General
{
    public class Contact : BaseEntity
    {
        public eContactType Type { get; set; }
        public string Value { get; set; }

        public ContactLabel ContactLabel { get; set; }
        public long ContactLabelId { get; set; }

        public Person Person { get; set; }
        public long PersonId { get; set; }
    }
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact", "General");
            builder.HasOne(o => o.ContactLabel).WithMany(m => m.Contacts).HasForeignKey(f => f.ContactLabelId).OnDelete(DeleteBehavior.Cascade).IsRequired(true);
            builder.HasOne(o => o.Person).WithMany(m => m.Contacts).HasForeignKey(f => f.PersonId).OnDelete(DeleteBehavior.Cascade).IsRequired(true);
        }
    }
}
