using CRM.Infrastructure.Persistance.Repositories;
using CRM.Infrastructure.Persistance.Repositories.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Persistance.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        private IUserRepository _users;
        public IUserRepository Users => _users == null ? _users = new UserRepository(_db) : _users;

        private ICustomerRepository _customers;
        public ICustomerRepository Customers => _customers == null ? _customers = new CustomerRepository(_db) : _customers;

        private IStaffRepository _staffs;
        public IStaffRepository Staffs => _staffs == null ? _staffs = new StaffRepository(_db) : _staffs;

        //private ISettingRepository _settings;
        //public ISettingRepository Settings => _settings == null ? _settings = new SettingRepository(_db) : _settings;

        //private IAccessRepository _accesses;
        //public IAccessRepository Accesses => _accesses == null ? _accesses = new AccessRepository(_db) : _accesses;

        //private IMenuAccessRepository _menuAccesses;
        //public IMenuAccessRepository MenuAccesses => _menuAccesses == null ? _menuAccesses = new MenuAccessRepository(_db) : _menuAccesses;

        //private IUserRoleRepository _userRoles;
        //public IUserRoleRepository UserRoles => _userRoles == null ? _userRoles = new UserRoleRepository(_db) : _userRoles;

        //private IUserAccessRepository _userAccess;
        //public IUserAccessRepository UserAccesses => _userAccess == null ? _userAccess = new UserAccessRepository(_db) : _userAccess;

        //private IAccessRoleRepository _accessRole;
        //public IAccessRoleRepository AccessRoles => _accessRole == null ? _accessRole = new AccessRoleRepository(_db) : _accessRole;

        private IRoleRepository _roles;
        public IRoleRepository Roles => _roles == null ? _roles = new RoleRepository(_db) : _roles;

        //private IPermissionRepository _permission;
        //public IPermissionRepository Permissions => _permission == null ? _permission = new PermissionRepository(_db) : _permission;

        //private IUserPermissionRepository _userPermission;
        //public IUserPermissionRepository UserPermissions => _userPermission == null ? _userPermission = new UserPermissionRepository(_db) : _userPermission;

        //private IAccessPermissionRepository _accessPermission;
        //public IAccessPermissionRepository AccessPermissions => _accessPermission == null ? _accessPermission = new AccessPermissionRepository(_db) : _accessPermission;

        //private IRecoveryRepository _recovery;
        //public IRecoveryRepository Recoveries => _recovery == null ? _recovery = new RecoveryRepository(_db) : _recovery;

        private IDeviceRepository _devices;
        public IDeviceRepository Devices => _devices == null ? _devices = new DeviceRepository(_db) : _devices;

        //private IDeviceTypeRepository _deviceTypes;
        //public IDeviceTypeRepository DeviceTypes => _deviceTypes == null ? _deviceTypes = new DeviceTypeRepository(_db) : _deviceTypes;

        private IInvoiceRepository _invoices;
        public IInvoiceRepository Invoices => _invoices == null ? _invoices = new InvoiceRepository(_db) : _invoices;

        private IInquiryRepository _inquiries;
        public IInquiryRepository Inquiries => _inquiries == null ? _inquiries = new InquiryRepository(_db) : _inquiries;

        private IInquiryCallRepository _inquiryDates;
        public IInquiryCallRepository InquiryDates => _inquiryDates == null ? _inquiryDates = new InquiryCallRepository(_db) : _inquiryDates;

        //private ITicketRepository _tickets;
        //public ITicketRepository Tickets => _tickets == null ? _tickets = new TicketRepository(_db) : _tickets;

        //private ITicketTypeRepository _ticketTypes;
        //public ITicketTypeRepository TicketTypes => _ticketTypes == null ? _ticketTypes = new TicketTypeRepository(_db) : _ticketTypes;

        //private ITicketFlowRepository _ticketFlow;
        //public ITicketFlowRepository TicketFlows => _ticketFlow == null ? _ticketFlow = new TicketFlowRepository(_db) : _ticketFlow;




        public int Complete()
        {
            return _db.SaveChanges();
        }
        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
