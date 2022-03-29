//using AutoMapper;
//using CRM.Application.SecurityApplication.Models;
//using CRM.Infrastructure.Persistance.Core;
//using Kendo.Mvc.Extensions;
//using Kendo.Mvc.UI;
//using MediatR;
//using Microsoft.AspNetCore.DataProtection;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CRM.Application.SecurityApplication.Queries.FindAll.Staff
//{
//    public class StaffFindAllQueryHandler : IRequestHandler<StaffFindAllQuery, DataSourceResult>
//    {
//        private readonly IUnitOfWork _uow;


//        public StaffFindAllQueryHandler(IUnitOfWork uow)
//        {
//            _uow = uow;
//        }
        
//        public async Task<DataSourceResult> Handle(StaffFindAllQuery request, CancellationToken cancellationToken)
//        {
//            //var list = await _uow.Staffs.TableNoTracking
//            //    .ToDataSourceResultAsync(request.Request, staff => new StaffSelectViewModel
//            //    {
//            //        Id = staff.Id,
//            //        FirstName = staff.FirstName,
//            //        LastName = staff.LastName,
//            //        Gender = staff.Gender,
//            //        BirthDate = staff.BirthDate,
//            //        NationalCode = staff.NationalCode,
//            //        StaffCode = staff.StaffCode
//            //    }, cancellationToken);
//            //return list;
//            return null;
//        }
//    }
//}
