using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CRM.Entities.DataModels.General;
using System.Collections.Generic;
using CRM.Entities.Core;

namespace CRM.Entities.DataModels.Security
{
    public class User : IdentityUser<Guid>
    {
        public DateTime? LastLoginDate { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
        public ICollection<UserAccess> UserAccesses { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public string TempCode { get; set; }
        public DateTime ExpireTempCodeTime { get; set; }

    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Security");
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(200);
            builder.HasOne(p => p.Person).WithOne(m => m.User)
                .HasForeignKey<User>(f => f.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
        }
    }
}
