using hamituslukan.PaymentSystem.Data.Interfaces;
using hamituslukan.PaymentSystem.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.Data.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly PaymentSystemContext _context;

        public Repository(PaymentSystemContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> sortingKey)
        {
            return await _context.Set<TEntity>().OrderByDescending(sortingKey).ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> sortingKey)
        {
            return await _context.Set<TEntity>().Where(expression).OrderByDescending(sortingKey).ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}