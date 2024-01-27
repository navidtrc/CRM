using CRM.Common.Api;
using CRM.Repository.Core;
using CRM.Service.Ticket;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
        //public async Task<ResultContent<DataSourceResult>> GetTicketsAsync(DataSourceRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await _uow.invoice.TableNoTracking
        //                .Where(w => w.IsDeleted == false)
        //                .Include(i => i.Person.User)
        //                .ToDataSourceResultAsync(request, cancellationToken);
        //    return new ResultContent<DataSourceResult>(true, result);
        //}
    }
}
