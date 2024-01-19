using CRM.Entities.HelperModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.MenuAccess
{
  public interface IMenuAccessService
  {
        Task UpdateMenuAccessTable(List<Entities.DataModels.Security.MenuAccess> accesses);
        Task<List<IGrouping<string, MenuGroupHelper>>> GetMenues();
  }
}
