using Imageverse.Application.Common.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class Repository<T, TId> : IRepository<T, TId>
        where T : class
        where TId : class
    {
        protected readonly ImageverseDbContext _dbContext;
        protected readonly DbSet<T> _entityDbSet;

        public Repository(ImageverseDbContext dbContext)
        {
            _dbContext = dbContext;
            _entityDbSet = _dbContext.Set<T>();
        }

        public async Task<T?> FindByIdAsync(TId id)
        {
            return await _entityDbSet.FindAsync(id);
        }

        public async Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entityDbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entityDbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _entityDbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllById(IEnumerable<TId> entityIds)
        {
            List<T> entities = new();
            foreach(var entityId in entityIds)
            {
                T? entity = await _entityDbSet.FindAsync(entityId);
                entities.Add(entity!);
            }
            return entities;
        }

        public async Task AddAsync(T entity)
        {
            await _entityDbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await _entityDbSet.AddRangeAsync(entity);
        }

        public void Delete(T entity)
        {
            _entityDbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _entityDbSet.Update(entity);
        }   
    }
}
