using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Commands.CreatePackage
{
    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, ErrorOr<Package>>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IDatabaseLogger _databaseLogger;

        public CreatePackageCommandHandler(IPackageRepository packageRepository, IDatabaseLogger databaseLogger)
        {
            _packageRepository = packageRepository;
            _databaseLogger = databaseLogger;
        }

        public async Task<ErrorOr<Package>> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            Package package = Package.Create(
                request.Name,
                request.Price,
                request.UploadSizeLimit,
                request.DailyUploadLimit,
                request.DailyImageUploadLimit);
            await _packageRepository.AddAsync(package);
            return package;
        }
    }
}
