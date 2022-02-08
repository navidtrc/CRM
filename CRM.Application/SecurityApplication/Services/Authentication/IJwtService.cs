using CRM.Application.SecurityApplication.Models;
using CRM.Domain.Models.Security;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Services.Authentication
{
    public interface IJwtService
    {
        Task<AccessTokenViewModel> GenerateAsync(User user);
    }
}