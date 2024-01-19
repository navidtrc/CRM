using CRM.Common;
using CRM.Common.Exceptions;
using CRM.Common.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using CRM.Repository.Core.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CRM.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Authorization;
using CRM.WebFramework.Filters;
using Newtonsoft.Json.Serialization;
using ElmahCore.Mvc;
using ElmahCore.Sql;

namespace CRM.WebFramework.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CrmMainDb"));
            });
        }

        public static void AddMinimalMvc(this IServiceCollection services)
        {
            services.AddControllersWithViews(option =>
            {
                option.Filters.Add(new AuthorizeFilter());
                option.Filters.Add(new ApiResultFilterAttribute());
            }).AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });            
        }

        public static void AddElmah(this IServiceCollection services, IConfiguration configuration, SiteSetting siteSetting)
        {
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.Path = siteSetting.ElmahPath;
                options.ConnectionString = configuration.GetConnectionString("Elmah");
            });
        }
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSetting jwtSetting)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(jwtSetting.SecretKey);
                var encryptkey = Encoding.UTF8.GetBytes(jwtSetting.Encryptkey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = jwtSetting.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = jwtSetting.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptkey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("Authentication failed.", context.Exception);

                        if (context.Exception != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<Entities.DataModels.Security.User>>();
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (!securityStamp.HasValue())
                            context.Fail("This token has no secuirty stamp");

                        //Find user and token from database and perform your custom validation
                        var userId = claimsIdentity.GetUserId<string>();
                        var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, Guid.Parse(userId));

                        //if (user.SecurityStamp != securityStamp)
                        //    context.Fail("Token secuirty stamp is not valid.");

                        var validateduser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validateduser == null)
                            context.Fail("token secuirty stamp is not valid.");

                        if (user.LockoutEnabled)
                            context.Fail("User is not active.");

                        await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                    },
                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                    }
                };
            });

        }
    }
}
