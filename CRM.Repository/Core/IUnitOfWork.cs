﻿using CRM.Repository.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ISettingRepository Settings { get; }
        IAccessRepository Accesses { get; }
        IMenuAccessRepository MenuAccesses { get; }
        IUserRoleRepository UserRoles { get; }
        IUserAccessRepository UserAccesses { get; }
        IAccessRoleRepository AccessRoles { get; }
        IRoleRepository Roles { get; }
        IUserPermissionRepository UserPermissions { get; }
        IPermissionRepository Permissions { get; }
        IAccessPermissionRepository AccessPermissions { get; }
        IRecoveryRepository Recoveries { get; }
        IPersonRepository People { get; }
        IStaffRepository Staffs { get; }
        ICustomerRepository Customers { get; }
        ITicketRepository Tickets { get; }
        IDeviceTypeRepository DeviceTypes { get; }
        IDeviceBrandRepository DeviceBrands { get; }
        IDeviceRepository Devices { get; }
        IFellowRepository Fellows { get; }

        int Complete();
        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
