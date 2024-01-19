using CRM.Common.Enums;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace CRM.Entities.DataModels.Security
{
    public class Access : BaseEntity
    {
        public int AccessCode { get; set; }
        public eAccessType AccessType { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Route { get; set; }
        public int Index { get; set; }
        public bool AllowAnonymous { get; set; } = false;

        public ICollection<AccessPermission> AccessPermission { get; set; }
        public ICollection<AccessRole> AccessRoles { get; set; }
        public ICollection<UserAccess> UserAccesses { get; set; }

    }
    public class AccessConfiguration : IEntityTypeConfiguration<Access>
    {
        public void Configure(EntityTypeBuilder<Access> builder)
        {
            builder.ToTable("Access", "Security");
        }
    }
}
