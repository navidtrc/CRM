using AutoMapper;

namespace CRM.Application.WebFramework.CustomMapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
