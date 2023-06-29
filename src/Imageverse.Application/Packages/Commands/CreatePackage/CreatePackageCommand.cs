using ErrorOr;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Commands.CreatePackage
{
    public record CreatePackageCommand(
        string Name,
        double Price,
        int UploadSizeLimit,
        int DailyUploadLimit,
        int DailyImageUploadLimit) : IRequest<ErrorOr<Package>>;
}
