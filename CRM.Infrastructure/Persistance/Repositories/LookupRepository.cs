using Common;
using CRM.Domain.Models;
using CRM.Infrastructure.Persistance.Core;
using CRM.Infrastructure.Persistance.Repositories.Core;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Persistance.Repositories
{
    public class LookupRepository : Repository<Lookup>, ILookupRepository
    {
        public LookupRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<OperationResult> AddTypeValueAsync(string typeName, string aux1, CancellationToken cancellationToken, string aux2 = null, string aux3 = null)
        {
            int nextId = 0;
            if (TableNoTracking.Any(a => a.TypeTitle == typeName))
                nextId = TableNoTracking.Where(w => w.TypeTitle == typeName).OrderByDescending(d => d.TypeId).FirstOrDefault().TypeId;
            await Entities.AddAsync(new Lookup
            {
                TypeTitle = typeName,
                Aux1 = aux1,
                Aux2 = aux2,
                Aux3 = aux3,
                TypeId = nextId + 1
            }, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return new OperationResult(true);
        }
        public OperationResult AddTypeValue(string typeName, string aux1, string aux2 = null, string aux3 = null)
        {
            int nextId = 0;
            if (TableNoTracking.Any(a => a.TypeTitle == typeName))
                nextId = TableNoTracking.Where(w => w.TypeTitle == typeName).OrderByDescending(d => d.TypeId).FirstOrDefault().TypeId;
            Entities.Add(new Lookup
            {
                TypeTitle = typeName,
                Aux1 = aux1,
                Aux2 = aux2,
                Aux3 = aux3,
                TypeId = nextId + 1
            });
            DbContext.SaveChanges();
            return new OperationResult(true);
        }
        public OperationResult<Lookup> GetTypeValueById(string typeName, int typeId)
        {
            var lookup = Table.FirstOrDefault(f => f.TypeTitle == typeName && f.TypeId == typeId);
            return new OperationResult<Lookup>(true, lookup);
        }
        public OperationResult<IQueryable<Lookup>> GetTypeValues(string typeName)
        {
            var lookups = Table.Where(f => f.TypeTitle == typeName);
            return new OperationResult<IQueryable<Lookup>>(true, lookups);
        }
        public async Task<OperationResult> UpdateTypeValueAsync(string typeName, int typeId, string aux1, CancellationToken cancellationToken, string aux2, string aux3)
        {
            var lookup = Table.FirstOrDefault(f => f.TypeTitle == typeName && f.TypeId == typeId);
            if (lookup == null)
                return new OperationResult(false, "lookup is not available");
            lookup.Aux1 = aux1;
            lookup.Aux2 = aux2;
            lookup.Aux3 = aux3;
            await UpdateAsync(lookup, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return new OperationResult(true);
        }
        public OperationResult UpdateTypeValue(string typeName, int typeId, string aux1, string aux2, string aux3)
        {
            var lookup = Table.FirstOrDefault(f => f.TypeTitle == typeName && f.TypeId == typeId);
            if (lookup == null)
                return new OperationResult(false, "lookup is not available");
            lookup.Aux1 = aux1;
            lookup.Aux2 = aux2;
            lookup.Aux3 = aux3;
            Update(lookup);
            DbContext.SaveChanges();
            return new OperationResult(true);
        }
        public async Task<OperationResult> DeleteTypeValueAsync(string typeName, int typeId, CancellationToken cancellationToken)
        {
            var lookup = Table.FirstOrDefault(f => f.TypeTitle == typeName && f.TypeId == typeId);
            if (lookup == null)
                return new OperationResult(false, "lookup is not available");
            await DeleteAsync(lookup, cancellationToken);
            return new OperationResult(true);
        }
        public OperationResult DeleteTypeValue(string typeName, int typeId)
        {
            var lookup = Table.FirstOrDefault(f => f.TypeTitle == typeName && f.TypeId == typeId);
            if (lookup == null)
                return new OperationResult(false, "lookup is not available");
            Delete(lookup);
            return new OperationResult(true);
        }
        public OperationResult<bool> ExistType(string typeName)
        {
            var isExist = TableNoTracking.Any(a => a.TypeTitle == typeName);
            return new OperationResult<bool>(true, isExist);
        }
        public OperationResult<bool> ExistType(string typeName, int typeId)
        {
            var isExist = TableNoTracking.Any(a => a.TypeTitle == typeName && a.TypeId == typeId);
            return new OperationResult<bool>(true, isExist);
        }
    }
}
