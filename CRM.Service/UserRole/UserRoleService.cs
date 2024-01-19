using CRM.Repository.Core;
using CRM.Service.User;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.UserRole
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;

        public UserRoleService(IUnitOfWork uow, IUserService userService, IMemoryCache memoryCache) 
        {
            this._uow = uow;
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        
        public async Task<int> ChangeUserRolesAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken)
        {
            var userRoles = await _uow.UserRoles.Table.Where(w => w.UserId == viewModel.UserId).ToListAsync();

            foreach (var role in userRoles)
                if (!viewModel.Permissions.Any(a => a.Equals(role.RoleId)))
                {
                    var defaultAccess = await _uow.AccessRoles.Table.Where(w => w.RoleId == role.RoleId).Select(s => s.AccessId).ToListAsync();
                    var useraccesses = await _uow.UserAccesses.Table.Where(w => w.UserId == viewModel.UserId).ToListAsync();
                    foreach (var item in useraccesses)
                    {
                        if (defaultAccess.Contains(item.AccessId))
                            await _uow.UserAccesses.DeleteAsync(item, cancellationToken, true);
                    }
                    await _uow.UserRoles.DeleteAsync(role, cancellationToken, true);
                }

            foreach (var roleId in viewModel.Permissions)
                if (!userRoles.Any(a => a.RoleId == (Guid)roleId))
                {
                    await _uow.UserRoles.AddAsync(new Entities.DataModels.Security.UserRole
                    {
                        RoleId = (Guid)roleId,
                        UserId = viewModel.UserId,
                    }, cancellationToken);

                    var defaultRoleAccesses = await _uow.AccessRoles.Table.Where(w => w.RoleId == (Guid)roleId).Select(s => new Entities.DataModels.Security.UserAccess
                    {
                        AccessId = s.AccessId,
                        UserId = viewModel.UserId,
                        IsActive = true,
                    }).ToListAsync();
                    if (defaultRoleAccesses.Count > 0)
                    {
                        var validUserAccesses = await _uow.UserAccesses.Table.Where(a => a.UserId == viewModel.UserId).ToListAsync();
                        foreach (var defaultRole in defaultRoleAccesses)
                            if (!validUserAccesses.Any(a => a.AccessId == defaultRole.AccessId))
                                await _uow.UserAccesses.AddRangeAsync(defaultRoleAccesses, cancellationToken);
                    }
                }
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);
        }

        public async Task<DataSourceResult> GetRolesForUserAsync_DataSourceResult(Guid userId, DataSourceRequest request)
        {
            var result = await _uow.Roles.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                p.Id,
                Permission = p.Title,
                Controller = "",
                Action = "",
                Route = "",
                HasPermission = _uow.UserRoles.TableNoTracking.Any(a => a.UserId == userId && a.RoleId == p.Id) ? true : false
            });
            return result;
        }

        private async Task<bool> AddAccessForRole(Entities.DataModels.Security.UserRole userRole, CancellationToken cancellationToken)
        {
            var accesses = _uow.AccessRoles.Table.Where(w => w.RoleId == userRole.RoleId)
                .Select(s => new Entities.DataModels.Security.UserAccess
                {
                    AccessId = s.AccessId,
                    UserId = userRole.UserId,
                });
            await _uow.UserAccesses.AddRangeAsync(accesses, cancellationToken);
            return true;
        }
        private async Task<bool> RemoveAccessForRole(Entities.DataModels.Security.UserRole userRole, CancellationToken cancellationToken)
        {
            var query = from defaultRoleAccess in _uow.AccessRoles.Table
                        join userAccess in _uow.UserAccesses.Table
                        on defaultRoleAccess.AccessId equals userAccess.AccessId
                        where userAccess.UserId == userRole.UserId
                        select userAccess;

            await _uow.UserAccesses.DeleteRangeAsync(query, cancellationToken);
            return true;
        }

        public async Task<DataSourceResult> GetUsersInRolesAsync_DataSourceResult(Guid roleID, DataSourceRequest request)
        {
            var result = await _uow.Users.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                p.Id,
                HasPermission = _uow.UserRoles.TableNoTracking.Any(a => a.UserId == p.Id && a.RoleId == roleID) ? true : false
            });
            return result;
        }

        public async Task<int> ChangeUserRolesReverseModeAsync(SavePermissionReverseViewModel viewModel, CancellationToken cancellationToken)
        {
            var userRoles = await _uow.UserRoles.Table.Where(w => w.Id == (Guid)viewModel.PermissionId).ToListAsync();

            foreach (var userRole in userRoles)
                if (!viewModel.Users.Any(a => a.Equals(userRole.UserId)))
                    await _uow.UserRoles.DeleteAsync(userRole, cancellationToken, true);

            foreach (var userId in viewModel.Users)
                if (!userRoles.Any(a => a.UserId == userId))
                    await _uow.UserRoles.AddAsync(new Entities.DataModels.Security.UserRole
                    {
                        RoleId = (Guid)viewModel.PermissionId,
                        UserId = userId,
                        IsActive = true,
                    }, cancellationToken);
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);
        }
    }
}
