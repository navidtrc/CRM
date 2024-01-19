using CRM.DAL;
using CRM.Repository.Core;
using CRM.Repository.Core.Repositories;
using CRM.Repository.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Repository.Persistence
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

        private ISettingRepository _settings;
        public ISettingRepository Settings => _settings == null ? _settings = new SettingRepository(_db) : _settings;

        //private IInvoiceRepository _invoices;
        //public IInvoiceRepository Invoices => _invoices == null ? _invoices = new InvoiceRepository(_db) : _invoices;

        private IAccessRepository _accesses;
        public IAccessRepository Accesses => _accesses == null ? _accesses = new AccessRepository(_db) : _accesses;

        private IMenuAccessRepository _menuAccesses;
        public IMenuAccessRepository MenuAccesses => _menuAccesses == null ? _menuAccesses = new MenuAccessRepository(_db) : _menuAccesses;

        private IUserRoleRepository _userRoles;
        public IUserRoleRepository UserRoles => _userRoles == null ? _userRoles = new UserRoleRepository(_db) : _userRoles;

        private IUserAccessRepository _userAccess;
        public IUserAccessRepository UserAccesses => _userAccess == null ? _userAccess = new UserAccessRepository(_db) : _userAccess;

        private IAccessRoleRepository _accessRole;
        public IAccessRoleRepository AccessRoles => _accessRole == null ? _accessRole = new AccessRoleRepository(_db) : _accessRole;

        private IRoleRepository _roles;
        public IRoleRepository Roles => _roles == null ? _roles = new RoleRepository(_db) : _roles;

        private IPermissionRepository _permission;
        public IPermissionRepository Permissions => _permission == null ? _permission = new PermissionRepository(_db) : _permission;

        private IUserPermissionRepository _userPermission;
        public IUserPermissionRepository UserPermissions => _userPermission == null ? _userPermission = new UserPermissionRepository(_db) : _userPermission;

        private IAccessPermissionRepository _accessPermission;
        public IAccessPermissionRepository AccessPermissions => _accessPermission == null ? _accessPermission = new AccessPermissionRepository(_db) : _accessPermission;

        private IRecoveryRepository _recovery;
        public IRecoveryRepository Recoveries => _recovery == null ? _recovery = new RecoveryRepository(_db) : _recovery;

        private IPersonRepository _people;
        public IPersonRepository People => _people == null ? _people = new PersonRepository(_db) : _people;

        private IStaffRepository _staffs;
        public IStaffRepository Staffs => _staffs == null ? _staffs = new StaffRepository(_db) : _staffs;

        private ICustomerRepository _customers;
        public ICustomerRepository Customers => _customers == null ? _customers = new CustomerRepository(_db) : _customers;


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
