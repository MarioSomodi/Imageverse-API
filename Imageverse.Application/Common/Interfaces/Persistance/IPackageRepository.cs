using Imageverse.Domain.PackageAggregate;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IPackageRepository
    {
        Task<Package?> GetPacakgeById(string id);
        Task Add(Package package);
    }
}
