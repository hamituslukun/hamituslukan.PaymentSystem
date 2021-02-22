using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression = null);

        Task<List<TEntity>> GetAsync();

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> sortingKey);

        Task<List<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> sortingKey);
    }
}