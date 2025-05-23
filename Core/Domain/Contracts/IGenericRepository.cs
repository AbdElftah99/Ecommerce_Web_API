using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<TEntity?> GetAsync(Tkey id);
        Task<TEntity?> GetAsync(Specification<TEntity> spec);
        Task<int> CountAsync(Specification<TEntity> spec);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(Specification<TEntity> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
