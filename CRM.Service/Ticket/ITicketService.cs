﻿using CRM.Common.Api;
using Kendo.Mvc.UI;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Ticket
{
    public interface ITicketService
    {
        //Task<ResultContent<DataSourceResult>> GetInvoicesAsync(DataSourceRequest request);
        Task<int> Prerequisite(CancellationToken cancellationToken);
    }
}