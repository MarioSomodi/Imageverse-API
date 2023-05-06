namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IRepository<T, TId>
        where T : class
        where TId : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TId id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetSingleOrDefaultByPropertyValueAsync(string property, object value);
        Task<IEnumerable<T>> GetAllByPropertyValueAsync(string property, object value);
        Task SaveChangesAsync();
    }
}
