using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using CRM.Entities.DataModels.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Entities.DataModels.Security
{
    public class Person : BaseEntity
    {
        [Display(Name = "FirstName", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public eGender Gender { get; set; }

        [Display(Name = "NationalCode", ResourceType = typeof(Resource))]
        public string NationalCode { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Resource))]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "BirthDate", ResourceType = typeof(Resource))]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Avatar", ResourceType = typeof(Resource))]
        public byte[] Avatar { get; set; }

        [Display(Name = "PersonType", ResourceType = typeof(Resource))]
        public ePersonType ePersonType { get; set; }

        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public User User { get; set; }
        public ICollection<Contact> Contacts { get; set; }
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
