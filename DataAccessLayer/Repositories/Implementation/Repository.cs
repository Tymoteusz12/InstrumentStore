using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityObject, new()
    {
        protected readonly ApplicationDbContext Context;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await Context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsNoTracking();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty).AsNoTracking());
            return await query.ToListAsync();

        }

        public async Task<TEntity> GetByIdAsync(int id) => await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsNoTracking();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty).AsNoTracking());
            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }

        public virtual async Task Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            Context.SaveChanges();
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }

        public virtual void Edit(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
        }
    }
}
