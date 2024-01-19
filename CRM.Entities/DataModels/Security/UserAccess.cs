using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.DataModels.Security
{
    public class UserAccess : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }

        public Access Access { get; set; }
        public long AccessId { get; set; }
    }
    public class UserAccessConfiguration : IEntityTypeConfiguration<UserAccess>
    {
        public void Configure(EntityTypeBuilder<UserAccess> builder)
        {
            builder.ToTable("UserAccess", "Security");

            builder.HasOne(o => o.User)
                .WithMany(m => m.UserAccesses)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Access)
                .WithMany(m => m.UserAccesses)
                .HasForeignKey(f => f.AccessId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
