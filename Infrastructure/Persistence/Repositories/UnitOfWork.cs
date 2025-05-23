using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private ConcurrentDictionary<string, object> _repositories = [];
        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
       
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        => (_repositories.GetOrAdd(typeof(TEntity).Name
            , _ => new GenericRepository<TEntity, Tkey>(context)) as IGenericRepository<TEntity, Tkey>)!;
    }
}
