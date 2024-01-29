using System;
using System.ComponentModel.DataAnnotations;
using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Entities.DataModels.Security
{
    public class Person : BaseEntity
    {
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "PersonType", ResourceType = typeof(Resource))]
        public ePersonType PersonType { get; set; }

        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public User User { get; set; }
    }
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "Security");
            builder.HasOne(h => h.Staff).WithOne(c => c.Person)
                .HasForeignKey<Staff>(f => f.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);

            builder.HasOne(h => h.Customer).WithOne(c => c.Person)
                .HasForeignKey<Customer>(f => f.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
        }
    }
}
