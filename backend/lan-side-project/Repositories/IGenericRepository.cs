namespace lan_side_project.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task DeleteAsync(int id);
    Task UpdateAsync(T entity);
}