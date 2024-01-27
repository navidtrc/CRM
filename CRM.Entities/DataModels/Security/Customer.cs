using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.DataModels.Security
{
    public class Customer : BaseEntity
    {
        [Display(Name = "CustomerType", ResourceType = typeof(Resource))]
        public ePeopleType CustomerType { get; set; }

        [Display(Name = "CustomerCode", ResourceType = typeof(Resource))]
        public int CustomerCode { get; set; }
        public Person Person { get; set; }

    }
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "Security");
        }
    }
}
