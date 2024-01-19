using CRM.Common.Api;
using CRM.Repository.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Staff
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _uow;
        public StaffService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public async Task<ResultContent<DataSourceResult>> GetAsync(DataSourceRequest request, CancellationToken cancellationToken)
        {
            var result = await _uow.Staffs.TableNoTracking
                .Where(w => w.IsDeleted == false)
                .Include(i => i.Person.User)
                .ToDataSourceResultAsync(request, cancellationToken);
            return new ResultContent<DataSourceResult>(true, result);
        }
        public async Task<ResultContent> DeleteStaffAsync(long id, CancellationToken cancellationToken)
        {
            var person = await _uow.People.GetByIdAsync(cancellationToken,id);
            if (person != null)
            {
                await _uow.People.DeleteAsync(person, cancellationToken);
                await _uow.CompleteAsync(cancellationToken);
            }
            return new ResultContent(true);
        }
    }
}