using ErrorOr;
using Imageverse.Domain.PackageAggregate;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetAllPackages
{
    public record GetAllPackagesQuery() : IRequest<ErrorOr<IEnumerable<Package>>>;
}
