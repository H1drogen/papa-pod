using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using System.Linq.Expressions;

namespace Fdm.Common.Repository
{
    public interface IGenericRepository<T>
       where T : Model
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

        Task Delete(int id);

        Task<bool> DoesExistAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByIdNoTrackingAsync(int id);

        Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties);

        Task<T> InsertAsync(T entity);

        Task SaveChangesAsync();

        T Update(T entityToUpdate);
    }
}