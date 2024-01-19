using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using CRM.Common.Enums;
using System;
using System.Collections.Generic;
using CRM.Service.UserAccess;
using CRM.Service.MenuAccess;

namespace CRM.UI.StartupCustomization
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutoAccessTypes(this IServiceCollection services)
        {
            Assembly asm = Assembly.GetAssembly(typeof(ServiceCollectionExtensions));
            #region Access

            var controllersUserAccess = asm.GetTypes()
                    .Where(type => typeof(Microsoft.AspNetCore.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new Entities.DataModels.Security.Access
                    {
                        IsActive = true,
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        Route = x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("Route"))?.ConstructorArguments.SingleOrDefault().Value.ToString().ToLower(),
                        AccessCode = int.Parse(x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[0].Value.ToString()),
                        AccessType = (eAccessType)int.Parse(x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[1].Value.ToString()),
                        Index = int.Parse(x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[2].Value.ToString()),
                        AllowAnonymous = x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess"))?.ConstructorArguments[3] == null ? false : bool.Parse(x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[3].Value.ToString())
                    }).OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            var apiControllersUserAccess = asm.GetTypes()
                    .Where(type => type.BaseType == typeof(Microsoft.AspNetCore.Mvc.ControllerBase))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new Entities.DataModels.Security.Access
                    {
                        IsActive = true,
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        Route = x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("Route"))?.ConstructorArguments.SingleOrDefault().Value.ToString().ToLower(),
                        AccessCode = int.Parse(x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[0].Value.ToString()),
                        AccessType = (eAccessType)int.Parse(x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[1].Value.ToString()),
                        Index = int.Parse(x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[2].Value.ToString()),
                        AllowAnonymous = x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess"))?.ConstructorArguments[3] == null ? false : bool.Parse(x.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[3].Value.ToString())
                    }).OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
            var reflectedAccesses = controllersUserAccess.Concat(apiControllersUserAccess).ToList();
            var hashset = new HashSet<int>();
            foreach (var code in reflectedAccesses)
                if (!hashset.Add(code.AccessCode))
                     throw new ArgumentException($"Duplicate Access Code : {code}");
            services.BuildServiceProvider().GetService<IAccessService>().UpdateAccessTable(reflectedAccesses);

            #endregion

            #region Menu
            var list = new List<Entities.DataModels.Security.MenuAccess>();

            var controllersMenu = asm.GetTypes()
                    .Where(type => typeof(Microsoft.AspNetCore.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any());

            foreach (var action in controllersMenu)
            {
                if (action.GetCustomAttributesData().Any(f => f.AttributeType.Name.Contains("Menu")))
                {
                    list.Add(new Entities.DataModels.Security.MenuAccess
                    {
                        IsActive = true,
                        Route = action.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("Route"))?.ConstructorArguments.SingleOrDefault().Value.ToString().ToLower(),
                        AccessCode = int.Parse(action.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("UserAccess")).ConstructorArguments[0].Value.ToString()),
                        MenuCode = int.Parse(action.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("Menu")).ConstructorArguments[0].Value.ToString()),
                        Order = int.Parse(action.GetCustomAttributesData().FirstOrDefault(f => f.AttributeType.Name.Contains("Menu")).ConstructorArguments[2].Value.ToString())
                    });
                }
            }        
            
            services.BuildServiceProvider().GetService<IMenuAccessService>().UpdateMenuAccessTable(list);

            #endregion

        }
    }
}
