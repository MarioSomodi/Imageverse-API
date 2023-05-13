using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetAllPackages
{
    public class GetAllPackagesCommandHandler : IRequestHandler<GetAllPackagesCommand, ErrorOr<IEnumerable<Package>>>
    {
        private readonly IPackageRepository _packageRepository;

        public GetAllPackagesCommandHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<ErrorOr<IEnumerable<Package>>> Handle(GetAllPackagesCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Package> packages = await _packageRepository.GetAllAsync();
            return packages.ToList();
        }
    }
}
