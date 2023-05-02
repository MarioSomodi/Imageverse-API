using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.Errors;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ErrorOr<Package>>
    {
        private readonly IPackageRepository _packageRepository;

        public GetByIdQueryHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<ErrorOr<Package>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _packageRepository.GetPacakgeById(query.Id) is not Package package)
            {
                return Errors.Common.NotFound("Package");
            }
            return package;
        }
    }
}
