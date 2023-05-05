using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PackageAggregate.ValueObjects;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class PackageRepository : Repository<Package, PackageId>, IPackageRepository
    {
        public PackageRepository(ImageverseDbContext dbContext) : base (dbContext)
        {
        }
    }
}
