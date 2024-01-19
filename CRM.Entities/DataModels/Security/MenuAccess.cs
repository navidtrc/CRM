using CRM.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Entities.DataModels.Security
{
    public class MenuAccess : BaseEntity
    {
        public int MenuCode { get; set; }
        public int AccessCode { get; set; }
        public string Route { get; set; }
        public int Order { get; set; }
    }
    public class MenuAccessConfiguration : IEntityTypeConfiguration<MenuAccess>
    {
        public void Configure(EntityTypeBuilder<MenuAccess> builder)
        {
            builder.ToTable("MenuAccess", "Security");
        }
    }
}
