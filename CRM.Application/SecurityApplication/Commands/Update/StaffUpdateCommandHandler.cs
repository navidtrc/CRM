using AutoMapper;
using Common;
using Common.Utilities;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Commands.Update
{
    public class StaffUpdateCommandHandler : IRequestHandler<StaffUpdateCommand, OperationResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IDataProtector _protector;
        public StaffUpdateCommandHandler(IUnitOfWork uow, IDataProtectionProvider provider)
        {
            _uow = uow;
            _protector = provider.CreateProtector("be8867fc-c737-41fa-8696-eeadbf11c808");
        }

        public async Task<OperationResult> Handle(StaffUpdateCommand request, CancellationToken cancellationToken)
        {
            var id = int.Parse(_protector.Unprotect(request.EncryptedId));



            var config = new MapperConfiguration(cfg => cfg.CreateMap<StaffUpdateCommand, Staff>());
            Mapper mapper = new Mapper(config);
            var staff = await _uow.Staffs.GetByIdAsync(cancellationToken, request.Id);
            staff = mapper.Map(request, staff);
            await _uow.Staffs.UpdateAsync(staff, cancellationToken);
            return new OperationResult(true);
        }
    }
}
