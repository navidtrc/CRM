using CRM.Entities.DataModels.Security;
using System.Threading.Tasks;

namespace CRM.Service.Authentication
{
    public interface IJwtService
    {
        Task<string> Generate(Entities.DataModels.Security.User user);
    }
}