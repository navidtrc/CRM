using CRM.Common.Api;
using CRM.Common.Enums;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.UI;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.People
{
    public interface IPeopleService 
    {
        Task<ResultContent<DataSourceResult>> GetAsync(DataSourceRequest request, ePersonType personType, CancellationToken cancellationToken);
        Task<ResultContent<object>> CreateAsync(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken);
        Task<ResultContent> PutAsync(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken);
        Task<ResultContent> DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
