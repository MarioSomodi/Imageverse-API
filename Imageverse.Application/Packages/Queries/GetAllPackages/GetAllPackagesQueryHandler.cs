using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetAllPackages
{
    public class GetAllPackagesQueryHandler : IRequestHandler<GetAllPackagesQuery, ErrorOr<IEnumerable<Package>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPackagesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<IEnumerable<Package>>> Handle(GetAllPackagesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Package> packages = await _unitOfWork.GetRepository<IPackageRepository>().Get(orderBy: p => p.OrderBy(x => x.Price));
            return packages.ToList();
        }
    }
}
