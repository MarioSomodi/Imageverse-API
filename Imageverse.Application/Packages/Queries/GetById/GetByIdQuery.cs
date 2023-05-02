using ErrorOr;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetById
{
    public record GetByIdQuery(
            string Id
        ) : IRequest<ErrorOr<Package>>;
}
