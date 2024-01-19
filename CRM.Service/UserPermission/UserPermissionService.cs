using CRM.Repository.Core;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.UserPermission
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache memoryCache;

        public UserPermissionService(IUnitOfWork uow, IMemoryCache memoryCache)
        {
            this._uow = uow;
            this.memoryCache = memoryCache;
        }


        public async Task<DataSourceResult> GetPermissionGroupForUserAsync_DataSourceResult(Guid userID, DataSourceRequest request)
        {
            var result = await _uow.Permissions.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                p.Id,
                Permission = p.Title,
                Controller = "",
                Action = "",
                Route = "",
                HasPermission = _uow.UserPermissions.TableNoTracking.Any(a => a.UserId == userID && a.PermissionId == p.Id) ? true : false
            });
            return result;
        }
        public async Task<int> ChangeUserGroupAccessAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken)
        {
            var userPermissionGroup = await _uow.UserPermissions.Table.Where(w => w.UserId == viewModel.UserId).ToListAsync();

            foreach (var group in userPermissionGroup)
                if (!viewModel.Permissions.Any(a => a.Equals(group.PermissionId)))
                {
                    var accessPermissionGroup = await _uow.AccessPermissions.Table.Where(w => w.PermissionId == group.PermissionId).Select(s => s.AccessId).ToListAsync();
                    var useraccesses = await _uow.UserAccesses.Table.Where(w => w.UserId == viewModel.UserId).ToListAsync();
                    foreach (var item in useraccesses)
                    {
                        if (accessPermissionGroup.Contains(item.AccessId))
                            await _uow.UserAccesses.DeleteAsync(item, cancellationToken, true);
                    }
                    await _uow.UserPermissions.DeleteAsync(group, cancellationToken, true);
                }

            foreach (var permissionId in viewModel.Permissions)
                if (!userPermissionGroup.Any(a => a.PermissionId == (int)permissionId))
                {
                    await _uow.UserPermissions.AddAsync(new Entities.DataModels.Security.UserPermission
                    {
                        PermissionId = (int)permissionId,
                        UserId = viewModel.UserId,
                        IsActive = true,
                    }, cancellationToken);

                    var defaultAccessPermissionGroup = await _uow.AccessPermissions.Table.Where(w => w.PermissionId == (int)permissionId).Select(s => new Entities.DataModels.Security.UserAccess
                    {
                        AccessId = s.AccessId,
                        UserId = viewModel.UserId,
                        IsActive = true,
                    }).ToListAsync();
                    if (defaultAccessPermissionGroup.Count > 0)
                    {
                        var validUserAccesses = await _uow.UserAccesses.Table.Where(a => a.UserId == viewModel.UserId).ToListAsync();
                        foreach (var defaultPermission in defaultAccessPermissionGroup)
                            if (!validUserAccesses.Any(a => a.AccessId == defaultPermission.AccessId))
                                await _uow.UserAccesses.AddRangeAsync(defaultAccessPermissionGroup, cancellationToken);
                    }
                }
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);

        }

        public async Task<DataSourceResult> GetUsersInPermissionGroupAsync_DataSourceResult(int permissionId, DataSourceRequest request)
        {
            var result = await _uow.Users.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                p.Id,
                HasPermission = _uow.UserPermissions.TableNoTracking.Any(a => a.UserId == p.Id && a.PermissionId == permissionId) ? true : false
            });
            return result;
        }

        public async Task<int> ChangeUserGroupAccessReverseModeAsync(SavePermissionReverseViewModel viewModel, CancellationToken cancellationToken)
        {
            var permissionGroups = await _uow.UserPermissions.Table.Where(w => w.Id == (int)viewModel.PermissionId).ToListAsync();

            foreach (var permissionGroup in permissionGroups)
                if (!viewModel.Users.Any(a => a.Equals(permissionGroup.UserId)))
                    await _uow.UserPermissions.DeleteAsync(permissionGroup, cancellationToken, true);

            foreach (var userId in viewModel.Users)
                if (!permissionGroups.Any(a => a.UserId == userId))
                    await _uow.UserPermissions.AddAsync(new Entities.DataModels.Security.UserPermission
                    {
                        PermissionId = (int)viewModel.PermissionId,
                        UserId = userId,
                        IsActive = true,
                    }, cancellationToken);
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);
        }
    }
}
