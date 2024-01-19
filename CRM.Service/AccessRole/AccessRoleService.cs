using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using CRM.Common.Enums;
using CRM.Common.Utilities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CRM.Service.User;
using CRM.Repository.Core;
using CRM.ViewModels.ViewModels;

namespace CRM.Service.AccessRole
{
    public class AccessRoleService : IAccessRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserService userService;

        public AccessRoleService(IUnitOfWork uow,IUserService userService) 
        {
            this._uow = uow;
            this.userService = userService;
        }

        public async Task<int> ChangeAccessRoleAsync(SaveAccessRoleViewModel viewModel, CancellationToken cancellationToken)
        {
            var accessRoles = await _uow.AccessRoles.Table.Where(w => w.RoleId == viewModel.RoleId).ToListAsync();

            foreach (var access in accessRoles)
                if (!viewModel.Accesses.Any(a => a.Equals(access.AccessId)))
                    await _uow.AccessRoles.DeleteAsync(access, cancellationToken, true);

            foreach (var accessId in viewModel.Accesses)
                if (!accessRoles.Any(a => a.AccessId == accessId))
                    await _uow.AccessRoles.AddAsync(new Entities.DataModels.Security.AccessRole
                    {
                        AccessId= accessId,
                        RoleId = viewModel.RoleId,
                        IsActive = true,
                    }, cancellationToken);

            return await _uow.CompleteAsync(cancellationToken);



            //var accessRoles = await _uow.AccessRoles.Table.Where(w => w.RoleId == viewModel.RoleId).ToListAsync();

            //foreach (var access in accessRoles)
            //    if (!viewModel.Accesses.Any(a => a.Equals(access.AccessId)))
            //    {
            //        var usersWithThatAccess = await _uow.UserAccesses.Table.Where(w => w.AccessId == access.AccessId).ToListAsync();

            //        foreach (var userWithThatAccess in usersWithThatAccess)
            //        {
            //            if (accessPermissionGroup.Contains(item.AccessID))
            //                await _uow.UserAccesses.DeleteAsync(item, cancellationToken, true);
            //        }


            //        await _uow.AccessRoles.DeleteAsync(access, cancellationToken, true);
            //    }

            //foreach (var accessId in viewModel.Accesses)
            //    if (!accessRoles.Any(a => a.AccessID == accessId))
            //        await _uow.DefaultRoleAccess.InsertAsync(new DomainClass.Core.DefaultRoleAccess
            //        {
            //            AccessID = accessId,
            //            RoleID = viewModel.RoleID,
            //            IsActive = true,
            //            RegDate = DateTime.Now,
            //            RegUserID = userService.GetCurrentUserID(),
            //        }, cancellationToken);

            //return await _uow.CompleteAsync(cancellationToken);
        }
       
        public async Task<DataSourceResult> GetAccessesForRoleAsync_DataSourceResult(Guid roleID, DataSourceRequest request)
        {
            var result = await _uow.Accesses.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                p.Id,
                Access = ((eAccessControl)p.AccessCode).ToDisplay(),
                Controller = p.Controller,
                Action = p.Action,
                Route = p.Route,
                HasPermission = _uow.AccessRoles.TableNoTracking.Any(a => a.RoleId == roleID && a.AccessId == p.Id) ? true : false
            });
            return result;
        }
        
    }
}
