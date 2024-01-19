using Kendo.Mvc.UI;
using System.Threading.Tasks;

namespace CRM.Service.Permission
{
    public interface IPermissionService 
    {
        Task<DataSourceResult> GetAllPermissionGroup_DataSourceResult(DataSourceRequest request);
    }
}
