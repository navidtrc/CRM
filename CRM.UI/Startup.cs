using CRM.UI.StartupCustomization;
using CRM.Common;
using CRM.ViewModels.CustomMapping;
using CRM.WebFramework.Configuration;
using CRM.WebFramework.Configurations;
using CRM.WebFramework.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace CRM.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSetting)).Get<SiteSetting>();
            AutoMapperConfiguration.InitializeAutoMapper();
        }

        private readonly SiteSetting _siteSetting;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSetting>(Configuration.GetSection(nameof(SiteSetting)));

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(_siteSetting.IdentitySetting);

            services.AddMinimalMvc();

            services.AddRepositoryDependencies();

            services.AddServiceDependencies();

            services.AddJwtAuthentication(_siteSetting.JwtSetting);

            services.AddHttpContextAccessor();

            services.AddKendo();

            services.AddRazorPages();

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            }).AddJsonProtocol();

            //services.AddAutoAccessTypes();

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCustomExtensionHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseJwtTokenInCookieHandler();

            app.UseSpaStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();

            //app.UseUserAccessControlHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
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
