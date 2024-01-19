using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using CRM.Common.Enums;
using CRM.Common.Utilities;
using CRM.Entities.DataModels.Security;
using CRM.Repository.Core;
using CRM.Repository.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Service.UserAccess
{
    public class AccessService : IAccessService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAccessRepository _accessRepository;

        public AccessService(IUnitOfWork uow, IAccessRepository accessRepository)
        {
            this._uow = uow;
            this._accessRepository = accessRepository;
        }

        public async Task UpdateAccessTable(List<Access> accesses)
        {
            var createAccess = new List<Access>();
            var updateAccess = new List<Access>();
            var updateAccessHelper = new List<Access>();

            var accessInDb = await _uow.Accesses.Table.ToListAsync();
            accesses.ForEach(f =>
            {
                if (f.Route != null && !f.Route.StartsWith("/"))
                    f.Route = $"/{f.Route}";
                if (accessInDb.Any(a => a.AccessCode == f.AccessCode))
                {
                    var model = accessInDb.FirstOrDefault(s => s.AccessCode == f.AccessCode);

                    if ((model.Index != f.Index) ||
                    (model.Controller != f.Controller) ||
                    (model.Action != f.Action) ||
                    (model.Route != f.Route) ||
                    (model.AccessType != f.AccessType) ||
                    (model.AllowAnonymous != f.AllowAnonymous))

                    {
                        model.Index = f.Index;
                        model.Controller = f.Controller;
                        model.Action = f.Action;
                        model.Route = f.Route;
                        model.AccessType = f.AccessType;
                        model.AllowAnonymous = f.AllowAnonymous;
                        updateAccessHelper.Add(model);
                    }
                    accessInDb.Remove(model);
                }
                else
                {
                    if (!accessInDb.Any(a => a.AccessCode == f.AccessCode))
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
                Title = ((eAccessControl)s.AccessCode).ToDisplay()
            });
        }
    }
}
