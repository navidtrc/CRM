using AutoMapper;
using CRM.Application.WebFramework.CustomMapping;

namespace CRM.Application.WebFramework.Api
{
    public abstract class BaseViewModel<TViewModel, TEntity> : IHaveCustomMapping
        where TViewModel : class
        where TEntity : class
    {
        public TEntity ToEntity(IMapper mapper)
        {
            return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
        }

        public TEntity ToEntity(IMapper mapper, TEntity entity)
        {
            return mapper.Map(CastToDerivedClass(mapper, this), entity);
        }

        public static TViewModel FromEntity(IMapper mapper, TEntity model)
        {
            return mapper.Map<TViewModel>(model);
        }

        protected TViewModel CastToDerivedClass(IMapper mapper, BaseViewModel<TViewModel, TEntity> baseInstance)
        {
            return mapper.Map<TViewModel>(baseInstance);
        }

        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TViewModel, TEntity>();
            var viewModelType = typeof(TViewModel);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
                if (viewModelType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());

            CustomMappings(mappingExpression.ReverseMap());
        }
        public virtual void CustomMappings(IMappingExpression<TEntity, TViewModel> mapping)
        {
        }
    }
}
