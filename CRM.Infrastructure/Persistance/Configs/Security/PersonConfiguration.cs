using CRM.Domain.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.Persistance.Configs.Security
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "Security");
            builder.Property(p => p.Id).UseHiLo($"Sequence-{nameof(Person)}");

            builder.HasOne(o => o.User)
                .WithOne(o => o.Person)
                .HasForeignKey<Person>(f => f.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            //builder.OwnsOne(m => m.Address, a =>
            //{
            //    a.Property(p => p.Street).HasMaxLength(600)
            //        .HasColumnName("Address_Street")
            //        .HasDefaultValue("");
            //    a.Property(p => p.City).HasMaxLength(150)
            //        .HasColumnName("Address_City")
            //        .HasDefaultValue("");
            //    a.Property(p => p.Country).HasMaxLength(60)
            //        .HasColumnName("Address_Country")
            //        .HasDefaultValue("");
            //    a.Property(p => p.ZipCode).HasMaxLength(12)
            //        .HasColumnName("Address_ZipCode")
            //        .HasDefaultValue("");
            //});
        }
    }
}
