using CRM.ViewModels.ViewModels;
using Kendo.Mvc.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.UserPermission
{
    public interface IUserPermissionService 
    {
        Task<DataSourceResult> GetPermissionGroupForUserAsync_DataSourceResult(Guid userId, DataSourceRequest request);
        Task<int> ChangeUserGroupAccessAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken);
        Task<DataSourceResult> GetUsersInPermissionGroupAsync_DataSourceResult(int permissionGroupID,DataSourceRequest request);
        Task<int> ChangeUserGroupAccessReverseModeAsync(SavePermissionReverseViewModel viewModel, CancellationToken cancellationToken);
    }
}
