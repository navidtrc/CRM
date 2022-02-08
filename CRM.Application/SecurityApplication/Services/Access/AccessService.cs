using Common;
using Common.Enums;
using Common.Utilities;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Services.Access
{
    public class AccessService : IAccessService, IScopedDependency
    {
        private readonly IUnitOfWork _uow;
        private readonly IAccessRepository _accessRepository;

        public AccessService(IUnitOfWork uow, IAccessRepository accessRepository)
        {
            this._uow = uow;
            this._accessRepository = accessRepository;
        }

        public async Task UpdateAccessTable(List<Domain.Models.Security.Access> accesses)
        {
            var createAccess = new List<Domain.Models.Security.Access>();
            var updateAccess = new List<Domain.Models.Security.Access>();
            var updateAccessHelper = new List<Domain.Models.Security.Access>();

            var accessInDb = await _uow.Accesses.Table.ToListAsync();
            accesses.ForEach(f =>
            {
                if (f.Route != null && !f.Route.StartsWith("/"))
                    f.Route = $"/{f.Route}";
                if (accessInDb.Any(a => a.Code == f.Code))
                {
                    var model = accessInDb.FirstOrDefault(s => s.Code == f.Code);

                    if ((model.OrderIndex != f.OrderIndex) ||
                    (model.ControllerName != f.ControllerName) ||
                    (model.ActionName != f.ActionName) ||
                    (model.Route != f.Route) ||
                    (model.AccessType != f.AccessType) ||
                    (model.AllowAnonymous != f.AllowAnonymous))
                    {
                        model.OrderIndex = f.OrderIndex;
                        model.ControllerName = f.ControllerName;
                        model.ActionName = f.ActionName;
                        model.Route = f.Route;
                        model.AccessType = f.AccessType;
                        model.AllowAnonymous = f.AllowAnonymous;
                        updateAccessHelper.Add(model);
                    }
                    accessInDb.Remove(model);
                }
                else
                {
                    if (!accessInDb.Any(a => a.Code == f.Code))
                        createAccess.Add(f);
                }
            });

            if (createAccess.Count > 0)
                _accessRepository.AddRange(createAccess);
            if (updateAccessHelper.Count > 0)
                _accessRepository.UpdateRange(updateAccess);
        }
        public async Task<DataSourceResult> GetAllAccess_DataSourceResult(DataSourceRequest request)
        {
            return await _uow.Accesses.TableNoTracking.ToDataSourceResultAsync(request, s => new
            {
                s.Id,
                Title = ((eAccessControl)s.Code).ToDisplay()
            });
        }
    }
}
