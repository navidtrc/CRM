using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using CRM.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CRM.Entities.HelperModels;
using CRM.ViewModels.ViewModels;
using CRM.Common.Enums;
using CRM.Common.Utilities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CRM.Service.UserAccess
{
    public class UserAccessService : IUserAccessService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache memoryCache;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAccessService(IUnitOfWork uow,  IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            this._uow = uow;
            this.memoryCache = memoryCache;
            this.httpContextAccessor = httpContextAccessor;
        }


        public async Task<bool> CheckForAccess(string path, CancellationToken cancellationToken = default)
        {
            var key = "useraccess";
            var userId = Guid.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!memoryCache.TryGetValue(key, out List<AccessCache> data))
            {
                data = await _uow.UserAccesses.TableNoTracking.Include(i => i.Access).Select(s => new AccessCache
                {
                    allowanonymous = s.Access.AllowAnonymous,
                    route = s.Access.Route,
                    userid = s.UserId
                }).ToListAsync();

                var expirationOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(1),
                    Priority = CacheItemPriority.High,
                    AbsoluteExpiration = DateTime.Now.AddHours(3)
                };
                memoryCache.Set(key, data, expirationOptions);
            }
            var access = memoryCache.Get<List<AccessCache>>("useraccess").Where(a => a.route == path.ToLower()).ToList();
            if (access.Count == 0 || access.Any(any => any.allowanonymous))
                return true;
            return access.Any(a => a.userid.Equals(userId));
        }

        public async Task<int> ChangeUserAccessesAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken)
        {
            var userAccesses = await _uow.UserAccesses.Table.Where(w => w.UserId == viewModel.UserId).ToListAsync();

            foreach (var access in userAccesses)
                if (!viewModel.Permissions.Any(a => a.Equals(access.AccessId)))
                    await _uow.UserAccesses.DeleteAsync(access, cancellationToken, true);

            foreach (var accessId in viewModel.Permissions)
                if (!userAccesses.Any(a => a.AccessId == (long)accessId))
                    await _uow.UserAccesses.AddAsync(new Entities.DataModels.Security.UserAccess
                    {
                        AccessId = (long)accessId,
                        UserId = viewModel.UserId,
                        IsActive = true,
                    }, cancellationToken);
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);
        }
        public async Task<DataSourceResult> GetAccessesForUserAsync_DataSourceResult(Guid userId, DataSourceRequest request)
        {
            var result = await _uow.Accesses.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                ID = (int)p.Id,
                Permission = ((eAccessControl)p.AccessCode).ToDisplay(),
                Controller = p.Controller,
                Action = p.Action,
                Route = p.Route,
                HasPermission = _uow.UserAccesses.TableNoTracking.Any(a => a.UserId == userId && a.AccessId == p.Id) ? true : false
            });
            return result;
        }

        public async Task<DataSourceResult> GetUsersInAccessesAsync_DataSourceResult(int accessID, DataSourceRequest request)
        {
            var result = await _uow.Users.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                p.Id,
                HasPermission = _uow.UserAccesses.TableNoTracking.Any(a => a.UserId == p.Id && a.AccessId == accessID) ? true : false
            });
            return result;
        }

        public async Task<int> ChangeUserAccessesReverseModeAsync(SavePermissionReverseViewModel viewModel, CancellationToken cancellationToken)
        {
            var accesses = await _uow.UserAccesses.Table.Where(w => w.Id == (int)viewModel.PermissionId).ToListAsync();

            foreach (var access in accesses)
                if (!viewModel.Users.Any(a => a.Equals(access.UserId)))
                    await _uow.UserAccesses.DeleteAsync(access, cancellationToken, true);

            foreach (var userId in viewModel.Users)
                if (!accesses.Any(a => a.UserId == userId))
                    await _uow.UserAccesses.AddAsync(new Entities.DataModels.Security.UserAccess
                    {
                        AccessId = (long)viewModel.PermissionId,
                        UserId = userId,
                        IsActive = true,
                    }, cancellationToken);
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);
        }
    }
}
