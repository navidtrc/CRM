using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using CRM.Application.InvoiceApplication.ViewModels;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.InvoiceApplication.Queries.GetById
{
    public class GetInvoiceByIdCommandHandler : IRequestHandler<GetInvoiceByIdCommand, OperationResult<InvoiceViewModel>>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public GetInvoiceByIdCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<OperationResult<InvoiceViewModel>> Handle(GetInvoiceByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await uow.Invoices.TableNoTracking
                .Include(i => i.Devices).ThenInclude(i => i.DeviceBrand)
                .Include(i => i.Devices).ThenInclude(i => i.DeviceType)
                .Include(i => i.Devices).ThenInclude(i => i.Inquiry)
                .Include(i => i.Customer)
                .Include(i => i.State)
                .ProjectTo<InvoiceViewModel>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);            
            
            return new OperationResult<InvoiceViewModel>(true, result);
        }
    }
}
