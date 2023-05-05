using ErrorOr;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetById
{
    public record GetPackageByIdQuery(
            string Id
        ) : IRequest<ErrorOr<Package>>;
}
