using Autofac;
using Common;
using CRM.Application.Common;
using CRM.Application.SecurityApplication.Authentication.Login;
using CRM.Application.Services.Sms;
using CRM.Application.WebFramework.Configuration;
using CRM.Application.WebFramework.CustomMapping;
using CRM.Domain.Models.Security;
using CRM.Infrastructure.Persistance;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories;
using CRM.Infrastructure.Persistance.Repositories.Core;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;

namespace CRM.WebTest
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InitializeAutoMapper();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("CrmMainDb")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = _siteSetting.IdentitySettings.PasswordRequireDigit;
                options.Password.RequiredLength = _siteSetting.IdentitySettings.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = _siteSetting.IdentitySettings.PasswordRequireNonAlphanumeric; //#@!
                options.Password.RequireUppercase = _siteSetting.IdentitySettings.PasswordRequireUppercase;
                options.Password.RequireLowercase = _siteSetting.IdentitySettings.PasswordRequireLowercase;
                options.User.RequireUniqueEmail = _siteSetting.IdentitySettings.RequireUniqueEmail;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<User, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();

            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddMediatR(typeof(LoginCommand).GetTypeInfo().Assembly);
            services.AddFluentValidation(new List<Assembly> { typeof(LoginCommand).GetTypeInfo().Assembly });
            services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(CommitCommandPostProcessor<,>));
            services.AddEmailService(Configuration);
            //services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISmsService, SmsService>();
            //services.AddScoped<IAccessRepository, AccessRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Register Services to Autofac ContainerBuilder
            builder.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.IntializeDatabase();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
