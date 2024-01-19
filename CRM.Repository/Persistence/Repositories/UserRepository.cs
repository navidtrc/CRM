using CRM.Common.Exceptions;
using CRM.Common.Utilities;
using CRM.DAL;
using CRM.Repository.Core.Repositories;
using CRM.Entities.DataModels.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CRM.Common.Api;
using System.Resources;
using System.Reflection;
using CRM.Common.Resources.StringResources;

namespace CRM.Repository.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ResultContent<User>> GetByPhoneAndPass(string phone, string password, CancellationToken cancellationToken)
        {
            var user = await TableNoTracking.FirstOrDefaultAsync(f => f.UserName == phone, cancellationToken);
            if (user == null)
                return new ResultContent<User>(false, null, Resource.ResourceManager.GetString("UserNotFound"));
            if (user.PhoneNumberConfirmed == false)
                return new ResultContent<User>(false, null, Resource.ResourceManager.GetString("PhoneNotConfirmed"));
            return user.PasswordHash == SecurityHelper.GetSha256Hash(password) ? new ResultContent<User>(true, user) : new ResultContent<User>(false, null, Resource.ResourceManager.GetString("IncorrectUserOrPass"));
        }

        public Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();
            return UpdateAsync(user, cancellationToken);
        }

        public override void Update(User entity, bool saveNow = true)
        {
            entity.SecurityStamp = Guid.NewGuid().ToString();
            base.Update(entity, saveNow);
        }

        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTime.Now;
            return UpdateAsync(user, cancellationToken);
        }

        public Task ChangeLockout(User user, CancellationToken cancellationToken)
        {

            user.LastLoginDate = DateTime.Now;
            return UpdateAsync(user, cancellationToken);
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
            if (exists)
                throw new BadRequestException("نام کاربری تکراری است");

            user.SecurityStamp = Guid.NewGuid().ToString();
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }
    }
}
