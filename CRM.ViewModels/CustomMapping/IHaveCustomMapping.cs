using AutoMapper;

namespace CRM.ViewModels.CustomMapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
