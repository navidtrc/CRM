using Microsoft.Extensions.DependencyInjection;
using CRM.Repository.Core;
using CRM.Repository.Persistence;
using CRM.Repository.Persistence.Repositories;
using CRM.Service;
using CRM.Service.User;
using CRM.Service.Email;
using CRM.Service.Sms;
using CRM.Repository.Core.Repositories;
using CRM.DAL;
using CRM.Service.Authentication;
using CRM.Service.UserAccess;
using CRM.Service.AccessPermission;
using CRM.Service.AccessRole;
using CRM.Service.MenuAccess;
using CRM.Service.Permission;
using CRM.Service.Roles;
using CRM.Service.UserPermission;
using CRM.Service.UserRole;
using CRM.Service.Staff;
using CRM.Service.People;
using CRM.Service.Customer;
using CRM.Service.Ticket;

namespace CRM.WebFramework.Configuration
{
    public static class IoCContainer
    {
        public static void AddRepositoryDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccessRepository, AccessRepository>();
            services.AddScoped<IMenuAccessRepository, MenuAccessRepository>();
        }
        public static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISmsService, SmsService>();

            services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IAccessPermissionService, AccessPermissionService>();
            services.AddScoped<IAccessRoleService, AccessRoleService>();
            services.AddScoped<IMenuAccessService, MenuAccessService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserAccessService, UserAccessService>();
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            services.AddScoped<IUserRoleService, UserRoleService>();

            //services.AddScoped<IInvoiceService, InvoiceService>();
        }
    }
}
