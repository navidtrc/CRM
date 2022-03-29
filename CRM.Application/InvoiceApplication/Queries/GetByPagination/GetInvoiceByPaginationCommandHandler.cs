using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Enums;
using CRM.Application.InvoiceApplication.ViewModels;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.InvoiceApplication.Queries.GetByPagination
{
    public class GetInvoiceByPaginationCommandHandler : IRequestHandler<GetInvoiceByPaginationCommand, OperationResult<List<InvoiceViewModel>>>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public GetInvoiceByPaginationCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<OperationResult<List<InvoiceViewModel>>> Handle(GetInvoiceByPaginationCommand request, CancellationToken cancellationToken)
        {
            var query = uow.Invoices.TableNoTracking
                .Include(i => i.Devices).ThenInclude(i => i.DeviceBrand)
                .Include(i => i.Devices).ThenInclude(i => i.DeviceType)
                .Include(i => i.Devices).ThenInclude(i => i.Inquiry)
                .Include(i => i.Customer)
                .Include(i => i.State)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);
            if (request.Filters != null)
            {
                foreach (var filter in request.Filters)
                {
                    switch (filter.Condition)
                    {
                        case eFilterCondition.Contains:
                            query = query.Where(w => ((string)w[filter.Column]).ToLower().Contains(filter.Value.ToLower()));
                            break;

                        case eFilterCondition.Equals:
                            if (filter.Type == eFilterValueType.DateTime)
                            {
                                var date = filter.Value.ToGregorianDateTime().Date;
                                query = query.Where(w => (DateTime)w[filter.Column] == date);
                            }
                            else if (filter.Type == eFilterValueType.Number)
                                query = query.Where(w => (long)w[filter.Column] == long.Parse(filter.Value));
                            else
                                query = query.Where(w => ((string)w[filter.Column]).ToLower().Equals(filter.Value.ToLower()));
                            break;
                        case eFilterCondition.StartsWith:
                            query = query.Where(w => ((string)w[filter.Column]).ToLower().StartsWith(filter.Value.ToLower()));
                            break;
                        case eFilterCondition.EndsWith:
                            query = query.Where(w => ((string)w[filter.Column]).ToLower().EndsWith(filter.Value.ToLower()));
                            break;
                        case eFilterCondition.NotEquals:
                            if (filter.Type == eFilterValueType.DateTime)
                            {
                                var date = filter.Value.ToGregorianDateTime().Date;
                                query = query.Where(w => (DateTime)w[filter.Column] != date);
                            }
                            else if (filter.Type == eFilterValueType.Number)
                                query = query.Where(w => (long)w[filter.Column] != long.Parse(filter.Value));
                            else
                                query = query.Where(w => !((string)w[filter.Column]).ToLower().Equals(filter.Value.ToLower()));
                            break;
                        case eFilterCondition.GreaterThan:
                            if (filter.Type == eFilterValueType.DateTime)
                            {
                                var date = filter.Value.ToGregorianDateTime().Date;
                                query = query.Where(w => (DateTime)w[filter.Column] < date);
                            }
                            else if (filter.Type == eFilterValueType.Number)
                            {
                                var num = long.Parse(filter.Value);
                                query = query.Where(w => (long)w[filter.Column] < num);
                            }
                            break;
                        case eFilterCondition.GreaterThanOrEqual:
                            if (filter.Type == eFilterValueType.DateTime)
                            {
                                var date = filter.Value.ToGregorianDateTime().Date;
                                query = query.Where(w => (DateTime)w[filter.Column] <= date);
                            }
                            else if (filter.Type == eFilterValueType.Number)
                            {
                                var num = long.Parse(filter.Value);
                                query = query.Where(w => (long)w[filter.Column] <= num);
                            }
                            break;
                        case eFilterCondition.LessThan:
                            if (filter.Type == eFilterValueType.DateTime)
                            {
                                var date = filter.Value.ToGregorianDateTime().Date;
                                query = query.Where(w => (DateTime)w[filter.Column] > date);
                            }
                            else if (filter.Type == eFilterValueType.Number)
                            {
                                var num = long.Parse(filter.Value);
                                query = query.Where(w => (long)w[filter.Column] > num);
                            }
                            break;
                        case eFilterCondition.LessThanOrEqual:
                            if (filter.Type == eFilterValueType.DateTime)
                            {
                                var date = filter.Value.ToGregorianDateTime().Date;
                                query = query.Where(w => (DateTime)w[filter.Column] >= date);
                            }
                            else if (filter.Type == eFilterValueType.Number)
                            {
                                var num = long.Parse(filter.Value);
                                query = query.Where(w => (long)w[filter.Column] >= num);
                            }
                            break;
                    }
                }
            }
            var result = await query
                .OrderByDescending(o => o.Number)
                .ProjectTo<InvoiceViewModel>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new OperationResult<List<InvoiceViewModel>>(true, result);
        }
    }
}
