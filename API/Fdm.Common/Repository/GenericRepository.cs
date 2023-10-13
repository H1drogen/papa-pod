using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fdm.Common.Repository
{
    public abstract class GenericRepository<TContext, T> : IGenericRepository<T>
     where TContext : DbContext
     where T : Model
    {
        protected TContext Context;
        protected DbSet<T> dbSet;

        protected GenericRepository(TContext context)
        {
            Context = context;
            dbSet = context.Set<T>();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.AnyAsync(filter);
        }

        public virtual async Task Delete(int id)
        {
            T entityToDelete = await dbSet.FindAsync(id);

            dbSet.Remove(entityToDelete);
        }

        public virtual async Task<bool> DoesExistAsync(int id)
        {
            return await dbSet.AnyAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T> GetByIdNoTrackingAsync(int id)
        {
            return await dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> filter)
        {
            return await GetEntitiesAsync(filter, null, "");
        }

        public virtual async Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return await GetEntitiesAsync(filter, orderBy, "");
        }

        public virtual async Task<IEnumerable<T>> GetEntitiesAsync(
          Expression<Func<T, bool>> filter,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
          string includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            var result = dbSet.Add(entity);
            await Context.SaveChangesAsync();

            return result.Entity;
        }

        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual T Update(T entityToUpdate)
        {
            var result = dbSet.Update(entityToUpdate);
            Context.Entry(entityToUpdate).Property(x => x.CreatedDate).IsModified = false;

            return result.Entity;
        }
    }
}