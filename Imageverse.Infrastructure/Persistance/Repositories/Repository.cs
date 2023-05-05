using Imageverse.Application.Common.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddAsync(T entity)
        {
            await _entityDbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _entityDbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entityDbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TId id)
        {
            return await Task.Run(() => _entityDbSet.ToList().SingleOrDefault(e => e.GetType().GetProperty("Id")!.GetValue(e)!.Equals(id)));
        }

        public async Task<T?> GetSingleOrDefaultByPropertyValueAsync(string property, object value)
        {
            return await Task.Run(() => _entityDbSet.ToList().SingleOrDefault(e => e.GetType().GetProperty(property)!.GetValue(e)!.Equals(value)));
        }

        public async Task<IEnumerable<T>> GetAllByPropertyValueAsync(string property, object value)
        {
            return await Task.Run(() => _entityDbSet.ToList().Where(e => e.GetType().GetProperty(property)!.GetValue(e)!.Equals(value)));
        }

        public async Task UpdateAsync(T entity)
        {
            _entityDbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
