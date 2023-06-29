using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Commands.CreatePackage
{
    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, ErrorOr<Package>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatabaseLogger _databaseLogger;

        public CreatePackageCommandHandler(IDatabaseLogger databaseLogger, IUnitOfWork unitOfWork)
        {
            _databaseLogger = databaseLogger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Package>> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            Package package = Package.Create(
                request.Name,
                request.Price,
                request.UploadSizeLimit,
                request.DailyUploadLimit,
                request.DailyImageUploadLimit);
            await _unitOfWork.GetRepository<IPackageRepository>().AddAsync(package);
            await _unitOfWork.CommitAsync();
            return package;
        }
    }
}
