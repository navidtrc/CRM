using CRM.ViewModels.ViewModels;
using Kendo.Mvc.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.UserRole
{
    public interface IUserRoleService
    {
        Task<DataSourceResult> GetRolesForUserAsync_DataSourceResult(Guid userID, DataSourceRequest request);
        Task<int> ChangeUserRolesAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken);
        Task<DataSourceResult> GetUsersInRolesAsync_DataSourceResult(Guid roleID, DataSourceRequest request);
        Task<int> ChangeUserRolesReverseModeAsync(SavePermissionReverseViewModel viewModel, CancellationToken cancellationToken);
    }
}
