using System.Linq.Expressions;

namespace lan_side_project.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAsync(
         Expression<Func<T, bool>>? filter = null,
         Func<IQueryable<T>, IQueryable<T>>? include = null,
         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
         int? skip = null,
         int? take = null,
         bool asNoTracking = false
     );
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task DeleteAsync(int id);
    Task UpdateAsync(T entity);
}