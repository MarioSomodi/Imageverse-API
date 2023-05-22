using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetPackageById
{
    public class GetPackageByIdQueryHandler : IRequestHandler<GetPackageByIdQuery, ErrorOr<Package>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPackageByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Package>> Handle(GetPackageByIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IPackageRepository>().FindByIdAsync(PackageId.Create(id)) is not Package package)
            {
                return Errors.Common.NotFound(nameof(Package));
            }
            return package;
        }
    }
}
