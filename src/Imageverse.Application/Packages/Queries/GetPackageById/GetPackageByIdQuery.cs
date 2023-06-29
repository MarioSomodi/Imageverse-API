using ErrorOr;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetPackageById
{
    public record GetPackageByIdQuery(
            string Id
        ) : IRequest<ErrorOr<Package>>;
}
