using AutoMapper;
using Common;
using Common.Exceptions;
using Common.Utilities;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Commands.Create
{
    public class StaffCreateCommandHandler : IRequestHandler<StaffCreateCommand, OperationResult>
    {
        private readonly IUnitOfWork _uow;
        public StaffCreateCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<OperationResult> Handle(StaffCreateCommand request, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.GetByIdAsync(cancellationToken, request.UserId);
            if (!user.IsActive)
                throw new AppException("User is deactive!");
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StaffCreateCommand, Staff>());
            Staff staff = new Mapper(config).Map<Staff>(request);
            do
            {
                staff.StaffCode = CodeGenerator.Generate(5);
                if (!_uow.Staffs.TableNoTracking.Any(a => a.StaffCode == staff.StaffCode)) break;
            } while (true);
            await _uow.Staffs.AddAsync(staff, cancellationToken);
            return new OperationResult(true);
        }
    }
}
