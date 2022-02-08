using Common;
using Common.Exceptions;
using Common.Utilities;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }
        public async Task<OperationResult<User>> Find(string emailOrPhone, string password, IPasswordHasher<User> passwordHasher, CancellationToken cancellationToken)
        {
            var user = await TableNoTracking.FirstOrDefaultAsync(f => f.Email == emailOrPhone || f.PhoneNumber == emailOrPhone, cancellationToken);
            if (user == null)
                return new OperationResult<User>(false, null, "User not found");
            return user.PasswordHash == passwordHasher.HashPassword(user, password) ? new OperationResult<User>(true, user) : new OperationResult<User>(false, null, "Incorrect username or pass");
        }
        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            bool emailBase = user.Email != null ? true : false;
            var exists = true;
            switch (emailBase)
            {
                case true:
                    user.UserName = user.Email;
                    exists = await TableNoTracking.AnyAsync(p => p.Email == user.UserName);
                    break;
                case false:
                    user.UserName = user.PhoneNumber;
                    exists = await TableNoTracking.AnyAsync(p => p.PhoneNumber == user.UserName);
                    break;
            }
            if (exists)
                throw new BadRequestException("This user is already exist");
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = SecurityHelper.GetSha256Hash(password);
            await base.AddAsync(user, cancellationToken);
        }
    }
}
