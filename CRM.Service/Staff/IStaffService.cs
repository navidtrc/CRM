using CRM.Common.Api;
using Kendo.Mvc.UI;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Staff
{
    public interface IStaffService 
    {
        Task<ResultContent<DataSourceResult>> GetAsync(DataSourceRequest request, CancellationToken cancellationToken);
        Task<ResultContent> DeleteStaffAsync(long id, CancellationToken cancellationToken);
    }
}
