using CRM.Common.Api;
using CRM.Entities.DataModels.Security;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Repository.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task<ResultContent<User>> GetByPhoneAndPass(string phone, string password, CancellationToken cancellationToken);
        Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    }
}