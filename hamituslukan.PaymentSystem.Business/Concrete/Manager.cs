using hamituslukan.PaymentSystem.Business.Interfaces;
using hamituslukan.PaymentSystem.Data.Interfaces;
using hamituslukan.PaymentSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.Business.Concrete
{
    public class Manager<TEntity> : IService<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IRepository<TEntity> _repository;

        public Manager(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await _repository.FindAsync(expression);
        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.GetAsync(expression);
        }

        public async Task<List<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> sortingKey)
        {
            return await _repository.GetAsync<TKey>(sortingKey);
        }

        public async Task<List<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> sortingKey)
        {
            return await _repository.GetAsync<TKey>(expression, sortingKey);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}