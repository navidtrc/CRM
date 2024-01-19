using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using CRM.Common.Enums;
using CRM.Common.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CRM.Repository.Core;
using CRM.Entities.DataModels.Security;
using CRM.ViewModels.ViewModels;

namespace CRM.Service.AccessPermission
{
    public class AccessPermissionService :  IAccessPermissionService
    {
        private readonly IUnitOfWork _uow;

        public AccessPermissionService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public async Task<List<Access>> GetAll()
        {
            return await _uow.Accesses.TableNoTracking.ToListAsync();
        }
       
        public async Task<DataSourceResult> GetAccessesForPermissionGroupAsync_DataSourceResult(int permissionId, DataSourceRequest request)
        {
            var result = await _uow.Accesses.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                p.Id,
                Access = ((eAccessControl)p.AccessCode).ToDisplay(),
                Controller = p.Controller,
                Action = p.Action,
                Route = p.Route,
                HasPermission = _uow.AccessPermissions.TableNoTracking.Any(a => a.PermissionId == permissionId && a.AccessId == p.Id) ? true : false
            });
            return result;
        }
        public async Task<int> ChangeAccessPermissionGroupAsync(SaveAccessPermissionViewModel viewModel, CancellationToken cancellationToken)
        {
            var accessPermissionGroup = await _uow.AccessPermissions.Table.Where(w => w.PermissionId == viewModel.PermissionId).ToListAsync();

            foreach (var access in accessPermissionGroup)
                if (!viewModel.Accesses.Any(a => a.Equals(access.AccessId)))
                    await _uow.AccessPermissions.DeleteAsync(access, cancellationToken, true);

            foreach (var accessId in viewModel.Accesses)
                if (!accessPermissionGroup.Any(a => a.AccessId == accessId))
                    await _uow.AccessPermissions.AddAsync(new Entities.DataModels.Security.AccessPermission
                    {
                        AccessId = accessId,
                        PermissionId = viewModel.PermissionId,
                        IsActive = true,
                    }, cancellationToken);

            return await _uow.CompleteAsync(cancellationToken);
        }
    }
}
