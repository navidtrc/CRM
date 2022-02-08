using Common;
using System.Threading.Tasks;

namespace CRM.Infrastructure.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
