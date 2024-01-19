using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using CRM.Repository.Core;
using CRM.Repository.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entities.HelperModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CRM.Service.MenuAccess
{
    public class MenuAccessService : IMenuAccessService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMenuAccessRepository menuAccessRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuAccessService(IUnitOfWork uow, IMenuAccessRepository menuAccessRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._uow = uow;
            this.menuAccessRepository = menuAccessRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task UpdateMenuAccessTable(List<Entities.DataModels.Security.MenuAccess> menues)
        {
            var createMenu = new List<Entities.DataModels.Security.MenuAccess>();
            var updateMenu = new List<Entities.DataModels.Security.MenuAccess>();
            var updateMenuHelper = new List<Entities.DataModels.Security.MenuAccess>();
            var deleteMenu = new List<Entities.DataModels.Security.MenuAccess>();

            var menuesInDb = await _uow.MenuAccesses.Table.ToListAsync();
            menues.ForEach(f =>
            {
                if (f.Route != null && !f.Route.StartsWith("/"))
                    f.Route = $"/{f.Route}";
                if (menuesInDb.Any(a => a.MenuCode == f.MenuCode))
                {
                    var model = menuesInDb.FirstOrDefault(s => s.MenuCode == f.MenuCode);
                    if ((model.AccessCode != f.AccessCode) || (model.Order != f.Order) || (model.Route != f.Route))
                    {
                        model.AccessCode = f.AccessCode;
                        model.Order = f.Order;
                        model.Route = f.Route;
                        updateMenuHelper.Add(model);
                    }
                    menuesInDb.Remove(model);
                }
                else
                {
                    if (!menuesInDb.Any(a => a.MenuCode == f.MenuCode))
                        createMenu.Add(f);
                }
            });

            if (createMenu.Count > 0)
                menuAccessRepository.AddRange(createMenu);
            if (updateMenuHelper.Count > 0)
                menuAccessRepository.UpdateRange(updateMenu);
        }

        public async Task<List<IGrouping<string, MenuGroupHelper>>> GetMenues()
        {
            //    var currentUser = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //    var roles = await _uow.UserRoles.TableNoTracking.Include(i => i.Roles).Where(w => w.UserId == currentUser).Select(s => s.Role.Title).ToListAsync();
            //    var menuGroup = new List<string>();
            //    var menuGroupLink = new List<MenuGroupHelper>();

            //    foreach (string role in roles)
            //        role.Split('،').ToList().ForEach(f => menuGroup.Add(f));
            //    menuGroup = menuGroup.Distinct().ToList();

            //    var userAccesses = await _uow.UserAccesses.TableNoTracking.Include(i => i.Access).Where(w => w.UserId == currentUser)
            //        .Select(s => s.Access.AccessCode).ToListAsync();
            //    var userMenues = await _uow.MenuAccesses.TableNoTracking.Where(w => userAccesses.Contains(w.AccessCode)).ToListAsync();


            //    var data = await _uow.AccessRoles.TableNoTracking.Include(i => i.Role).Include(i => i.Access).Where(w => userAccesses.Contains(w.Access.AccessCode)).ToListAsync();
            //    foreach (var f in data)
            //    {
            //        if (userMenues.Any(a => a.AccessCode == f.Access.AccessCode))
            //        {
            //            //var descriptions = f.Role.Description.Split('،').ToList();
            //            //var descriptionsTemp = data.Where(w => w.Access.AccessCode == f.Access.AccessCode && w.RoleId != f.Role.Id).Select(s => s.Role.Description).ToList();

            //            //if (descriptionsTemp.Count > 0)
            //            //{
            //            //    var tempMenuGroup = new List<string>();
            //            //    foreach (string tempRole in descriptionsTemp)
            //            //        tempRole.Split('،').ToList().ForEach(f => tempMenuGroup.Add(f));
            //            //    tempMenuGroup = tempMenuGroup.Distinct().ToList();

            //            //    descriptions = descriptions.Intersect(tempMenuGroup).ToList();
            //            //}
            //            //else
            //            //{
            //            //    var defaultMenu = descriptions[0];
            //            //    descriptions.Clear();
            //            //    descriptions.Add(defaultMenu);
            //            //}


            //            //foreach (string item in descriptions)
            //            //{
            //            //    if (menuGroup.Contains(item))
            //            //    {
            //            //        var tempMenu = userMenues.FirstOrDefault(p => p.AccessCode == f.Access.AccessCode);
            //            //        if (tempMenu != null && (!menuGroupLink.Any(a => a.Name == ((eMenu)tempMenu.MenuCode).ToDisplay() && a.Parent == item)))
            //            //        {
            //            //            menuGroupLink.Add(new MenuGroupHelper
            //            //            {
            //            //                Parent = item,
            //            //                Name = ((eMenu)(tempMenu.MenuCode)).ToDisplay(),
            //            //                Route = tempMenu.Route
            //            //            });
            //            //        }
            //            //    }
            //            //}
            //        }

            //    }
            //    return menuGroupLink.GroupBy(g => g.Parent).ToList();
            return null;
        }
    }
}
