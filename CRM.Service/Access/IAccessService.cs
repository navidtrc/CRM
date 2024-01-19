using CRM.Entities.DataModels.Security;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Service.UserAccess
{
    public interface IAccessService 
    {
        Task UpdateAccessTable(List<Access> accesses);
        Task<DataSourceResult> GetAllAccess_DataSourceResult(DataSourceRequest request);
    }
}
