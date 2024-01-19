using CRM.ViewModels.ViewModels;
using Kendo.Mvc.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.UserAccess
{
    public interface IUserAccessService
    {
        Task<bool> CheckForAccess(string path, CancellationToken cancellationToken = default);
        Task<int> ChangeUserAccessesAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken);
        Task<DataSourceResult> GetAccessesForUserAsync_DataSourceResult(Guid userID, DataSourceRequest request);
        Task<DataSourceResult> GetUsersInAccessesAsync_DataSourceResult(int accessID, DataSourceRequest request);
        Task<int> ChangeUserAccessesReverseModeAsync(SavePermissionReverseViewModel viewModel, CancellationToken cancellationToken);
    }
}
