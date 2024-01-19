using CRM.Repository.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Threading.Tasks;

namespace CRM.Service.Roles
{
    public class RoleService :IRoleService
    {
        private readonly IUnitOfWork _uow;

        public RoleService(IUnitOfWork uow) 
        {
            this._uow = uow;
        }
        public async Task<DataSourceResult> GetAllRoles_DataSourceResult(DataSourceRequest request)
        {
            return await _uow.Roles.TableNoTracking.ToDataSourceResultAsync(request, s => new
            {
                s.Id,
                s.Title
            });
        }
    }
}
