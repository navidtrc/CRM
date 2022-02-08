using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Services.Access
{
    public interface IAccessService 
    {
        Task UpdateAccessTable(List<Domain.Models.Security.Access> accesses);
        Task<DataSourceResult> GetAllAccess_DataSourceResult(DataSourceRequest request);
    }
}
