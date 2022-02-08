using CRM.Infrastructure.Persistance.Repositories.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Persistance.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        //ISettingRepository Settings { get; }
        IInvoiceRepository Invoices { get; }
        //IAccessRepository Accesses { get; }
        //IMenuAccessRepository MenuAccesses { get; }
        //IUserRoleRepository UserRoles { get; }
        //IUserAccessRepository UserAccesses { get; }
        //IAccessRoleRepository AccessRoles { get; }
        IRoleRepository Roles { get; }
        //IUserPermissionRepository UserPermissions { get; }
        //IPermissionRepository Permissions { get; }
        //IAccessPermissionRepository AccessPermissions { get; }
        //IRecoveryRepository Recoveries { get; }
        IStaffRepository Staffs { get; }
        ICustomerRepository Customers { get; }

        IDeviceRepository Devices { get; }
        IDeviceTypeRepository DeviceTypes { get; }
        IInquiryRepository Inquiries { get; }
        IInquiryDateRepository InquiryDates { get; }
        ITicketRepository Tickets { get; }
        ITicketTypeRepository TicketTypes { get; }
        ITicketFlowRepository TicketFlows { get; }


        int Complete();
        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
