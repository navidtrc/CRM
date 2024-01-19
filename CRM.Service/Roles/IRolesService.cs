using Kendo.Mvc.UI;
using System.Threading.Tasks;

namespace CRM.Service.Roles
{
    public interface IRoleService 
    {
        Task<DataSourceResult> GetAllRoles_DataSourceResult(DataSourceRequest request);
    }
}
