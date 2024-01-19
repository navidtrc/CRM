using CRM.ViewModels.ViewModels;
using Kendo.Mvc.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.AccessRole
{
    public interface IAccessRoleService
    {
        Task<int> ChangeAccessRoleAsync(SaveAccessRoleViewModel viewModel, CancellationToken cancellationToken);
        Task<DataSourceResult> GetAccessesForRoleAsync_DataSourceResult(Guid roleID, DataSourceRequest request);
    }
}
