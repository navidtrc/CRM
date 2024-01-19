using System;
using System.ComponentModel.DataAnnotations;
using CRM.Common.Resources.StringResources;

namespace CRM.Entities.Core
{
    public interface IBaseEntity
    {
    }
    public abstract class BaseEntity<TKey> : IBaseEntity where TKey : struct
    {
        [Key]
        [Display(Name = "Id", ResourceType = typeof(Resource))]
        public virtual TKey Id { get; set; }

        [Display(Name = "Guid", ResourceType = typeof(Resource))]
        public virtual Guid Guid { get; set; } = Guid.NewGuid();

        [Display(Name = "CreatedDate", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resource))]
        public virtual DateTime? LastModifiedDate { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public virtual string Description { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public virtual bool IsActive { get; set; } = true;

        [Display(Name = "IsDeleted", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public virtual bool IsDeleted { get; set; } = false;
    }

    public abstract class BaseEntity : BaseEntity<long>
    {
    }
}
