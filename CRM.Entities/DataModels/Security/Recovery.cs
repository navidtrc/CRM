using CRM.Common.Enums;
using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace CRM.Entities.DataModels.Security
{
    public class Recovery : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RecoveryKey { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
    public class RecoveryConfiguration : IEntityTypeConfiguration<Recovery>
    {
        public void Configure(EntityTypeBuilder<Recovery> builder)
        {
            builder.ToTable("Recovery", "Security");
        }
    }
}
