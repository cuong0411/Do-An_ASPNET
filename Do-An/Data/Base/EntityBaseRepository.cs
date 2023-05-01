using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Do_An.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext appDbContext;

        public EntityBaseRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await appDbContext.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await appDbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
        public async Task AddAsync(T entity)
        {
            await appDbContext.Set<T>().AddAsync(entity);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await appDbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
            EntityEntry entityEntry = appDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = appDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = appDbContext.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }
    }
}
