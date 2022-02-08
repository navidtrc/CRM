using Common;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Persistance.Repositories.Core
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task<OperationResult<User>> Find(string emailOrPhone, string password, IPasswordHasher<User> passwordHasher, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    }
}