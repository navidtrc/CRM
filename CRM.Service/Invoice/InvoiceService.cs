//using CRM.Common.Api;
//using CRM.Repository.Core;
//using CRM.ViewModels.ViewModels;
//using Kendo.Mvc.Extensions;
//using Kendo.Mvc.UI;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace CRM.Service.Invoice
//{
//    public class InvoiceService : IInvoiceService
//    {
//        private readonly IUnitOfWork unitOfWork;
//        public InvoiceService(IUnitOfWork unitOfWork)
//        {
//            this.unitOfWork = unitOfWork;
//        }
//        public async Task<ResultContent<DataSourceResult>> GetInvoicesAsync(DataSourceRequest request)
//        {
//            var result = await unitOfWork.Invoices.TableNoTracking.Include(i => i.Status).ToDataSourceResultAsync(request,
//              p => new ShowInvoicesViewModel().FromEntityCustom(p));
//            return new ResultContent<DataSourceResult>(true, result);
//        }
//    }
//}
