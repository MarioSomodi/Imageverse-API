using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Infrastructure.Persistance;
using System.Reflection;

namespace Imageverse.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ImageverseDbContext _dbContext;
        private readonly List<IRepositoryMarker> _repositoryInstances = new();

        public UnitOfWork(ImageverseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        //Legacy should refactor
        public T GetRepository<T>() where T : class
        {
            Type typeApstraction = typeof(T);

            if (_repositoryInstances.Any(rI => rI.GetType().IsAssignableTo(typeApstraction)))
            {
                return (T)_repositoryInstances.Find(rI => rI.GetType().IsAssignableTo(typeApstraction))!;
            }

            Type typeImplementation = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(p => typeApstraction.IsAssignableFrom(p))
                .First();

            T repository = (T)Activator.CreateInstance(typeImplementation, _dbContext)!;
            _repositoryInstances.Add((IRepositoryMarker)repository);
                
            return repository;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
