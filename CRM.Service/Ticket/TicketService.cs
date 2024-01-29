using CRM.Common.Api;
using CRM.Repository.Core;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Ticket
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _uow;
        public TicketService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task<ResultContent<TicketPrerequisiteViewModel>> Prerequisite(CancellationToken cancellationToken)
        {
            var result = new TicketPrerequisiteViewModel();
            var types = await _uow.DeviceTypes.TableNoTracking.ToListAsync(cancellationToken);
            result.DeviceTypeList = types.Select(s => new DeviceTypeViewModel { id = s.Id, label = s.Title }).ToList();

            var brands = await _uow.DeviceBrands.TableNoTracking.ToListAsync(cancellationToken);
            result.DeviceBrandList = brands.Select(s => new DeviceBrandViewModel { id = s.Id, label = s.Title }).ToList();

            var last = await _uow.Tickets.TableNoTracking.OrderByDescending(o => o.Id).FirstOrDefaultAsync(cancellationToken);
            if (last == null)
                result.LastTicketNumber = 1;
            else
                result.LastTicketNumber = last.Number++;
            return new ResultContent<TicketPrerequisiteViewModel>(true, result);
        }

        public async Task<ResultContent<DataSourceResult>> GetTicketsAsync(DataSourceRequest request, CancellationToken cancellationToken)
        {
            var result = await _uow.Tickets.TableNoTracking
                        .Where(w => w.IsDeleted == false)
                        .Include(i => i.Customer)
                        .ThenInclude(i => i.Person)
                        .ThenInclude(i => i.User)
                        .ToDataSourceResultAsync(request, cancellationToken);
            return new ResultContent<DataSourceResult>(true, result);
        }
    }
}
