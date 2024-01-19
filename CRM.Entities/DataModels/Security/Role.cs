using CRM.Entities.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using CRM.Common.Resources.StringResources;
using System.Collections.Generic;

namespace CRM.Entities.DataModels.Security
{
    public class Role : IdentityRole<Guid>
    {
        public string Title { get; set; }
        public ICollection<AccessRole> AccessRoles { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role", "Security");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
