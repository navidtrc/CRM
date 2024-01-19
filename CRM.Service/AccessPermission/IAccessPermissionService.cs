using CRM.ViewModels.ViewModels;
using Kendo.Mvc.UI;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.AccessPermission
{
    public interface IAccessPermissionService
    {
        Task<DataSourceResult> GetAccessesForPermissionGroupAsync_DataSourceResult(int permissionGroupId, DataSourceRequest request);
        Task<int> ChangeAccessPermissionGroupAsync(SaveAccessPermissionViewModel viewModel, CancellationToken cancellationToken);
    }
}
