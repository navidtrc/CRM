using AutoMapper;
using CRM.Common.Resources.StringResources;
using CRM.Common.Utilities;
using CRM.Entities.Core;
using CRM.ViewModels.CustomMapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.ViewModels.Base
{
    public interface IBaseViewModel
    {
        public DateTime CreatedDate { get; set; }
        public string PersianCreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
    public abstract class BaseViewModel<TViewModel, TEntity, TKey> : IHaveCustomMapping, IBaseViewModel
        where TViewModel : IBaseViewModel
        where TEntity : BaseEntity<TKey>, new()
        where TKey : struct
    {
        [Display(Name = "Id", ResourceType = typeof(Resource))]
        public TKey Id { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public DateTime CreatedDate { get; set; }

        public string PersianCreatedDate { get; set; }

        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resource))]
        public DateTime? LastModifiedDate { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resource))]
        public bool IsActive { get; set; }

        /// <summary>
        /// Convert ViewModel to Entity for create operation
        /// </summary>
        /// <returns></returns>
        public virtual TEntity ToEntity()
        {
            var model = Mapper.Map<TEntity>(CastToDerivedClass(this));
            model.CreatedDate = model.CreatedDate == DateTime.MinValue ? DateTime.Now : model.CreatedDate;
            return model;
        }

        /// <summary>
        /// Convert ViewModel to Entity for update operation
        /// </summary>
        /// <param name="entity">The reflected entity from database that need to update</param>
        /// <returns></returns>
        public virtual TEntity ToEntity(TEntity entity) // For Update
        {
            entity.LastModifiedDate = DateTime.Now;
            return Mapper.Map(CastToDerivedClass(this), entity);
        }

        /// <summary>
        /// Convert Entity to ViewModel for read operation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TViewModel FromEntity(TEntity model)
        {
            return Mapper.Map<TViewModel>(model);
        }

        public virtual TViewModel FromEntityCustom(TEntity model)
        {
            var viewModel = Mapper.Map<TViewModel>(model);
            viewModel.PersianCreatedDate = viewModel.CreatedDate.ToPersianDate(true, true);
            return viewModel;
        }

        protected TViewModel CastToDerivedClass(BaseViewModel<TViewModel, TEntity, TKey> baseInstance)
        {
            return Mapper.Map<TViewModel>(baseInstance);
        }

        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TViewModel, TEntity>();

            var dtoType = typeof(TViewModel);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMappings(mappingExpression.ReverseMap());
        }

        public virtual void CustomMappings(IMappingExpression<TEntity, TViewModel> mapping)
        {
        }

    }
    public abstract class BaseViewModel<TViewModel, TEntity> : BaseViewModel<TViewModel, TEntity, long>
        where TViewModel : IBaseViewModel
        where TEntity : BaseEntity<long>, new()
    {

    }
}
