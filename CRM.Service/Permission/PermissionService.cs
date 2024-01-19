using CRM.Repository.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Threading.Tasks;

namespace CRM.Service.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _uow;

        public PermissionService(IUnitOfWork uow)
        {
            this._uow = uow;
        }


        public async Task<DataSourceResult> GetAllPermissionGroup_DataSourceResult(DataSourceRequest request)
        {
            return await _uow.Permissions.TableNoTracking.ToDataSourceResultAsync(request, s => new
            {
                s.Id,
                s.Title
            });
        }

    }
}
