using System.Linq.Expressions;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IRepository<T, TId> : IRepositoryMarker
        where T : class
        where TId : class
    {
        Task<T?> FindById(TId id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllById(IEnumerable<TId> entityIds);
        Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    }
}
