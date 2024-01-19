using CRM.Common;
using CRM.DAL;
using CRM.Entities.DataModels.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.WebFramework.Configurations
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySetting setting)
        {
            services.AddIdentity<User, Role>(options =>
            {
                // Password settings
                //Password Settings
                options.Password.RequireDigit = setting.PasswordRequireDigit;
                options.Password.RequiredLength = setting.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = setting.PasswordRequireNonAlphanumic;
                options.Password.RequireUppercase = setting.PasswordRequireUppercase;
                options.Password.RequireLowercase = setting.PasswordRequireLowercase;
                
                //UserName Settings
                options.User.RequireUniqueEmail = setting.RequireUniqueEmail;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
