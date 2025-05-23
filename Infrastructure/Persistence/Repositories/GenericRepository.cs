using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext context) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity) => await context.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false) =>
            trackChanges
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().AsNoTracking().ToListAsync();
        public async Task<IEnumerable<TEntity>> GetAllAsync(Specification<TEntity> spec) => await context.Set<TEntity>().GetQuery(spec).ToListAsync();

        public async Task<int> CountAsync(Specification<TEntity> spec) => await context.Set<TEntity>().GetQuery(spec).CountAsync();


        public async Task<TEntity?> GetAsync(Tkey id) => await context.Set<TEntity>().FindAsync(id);
        public async Task<TEntity?> GetAsync(Specification<TEntity> spec) => await context.Set<TEntity>().GetQuery(spec).FirstOrDefaultAsync();

        public void Delete(TEntity entity) => context.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);

       
    }
}
