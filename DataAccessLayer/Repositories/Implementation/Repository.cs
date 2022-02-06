using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await Context.Set<TEntity>().
                Where(x => x.Id == id)
                .FirstOrDefaultAsync();
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

        public void Edit(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
        }
    }
}
