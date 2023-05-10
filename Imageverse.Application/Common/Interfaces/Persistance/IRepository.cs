namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IRepository<T, TId>
        where T : class
        where TId : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TId id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T?> GetSingleOrDefaultByPropertyValueAsync(string property, object value);
        Task<IEnumerable<T>> GetAllByPropertyValueAsync(string property, object value);
        Task<bool> SaveChangesAsync();
    }
}
