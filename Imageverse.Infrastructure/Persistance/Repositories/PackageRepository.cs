using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PackageAggregate;
using Microsoft.EntityFrameworkCore;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly ImageverseDbContext _dbContext;

        public PackageRepository(ImageverseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Package package)
        {
            await _dbContext.Packages.AddAsync(package);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Package?> GetPacakgeById(string id)
        {
            return await _dbContext.Packages.SingleOrDefaultAsync(p => p.Id.Value == new Guid(id));
        }
    }
}
