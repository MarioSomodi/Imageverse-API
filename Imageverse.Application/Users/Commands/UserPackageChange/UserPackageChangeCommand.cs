using ErrorOr;
using MediatR;

namespace Imageverse.Application.Users.Commands.UserPackageChange
{
    public record UserPackageChangeCommand(
        string Id,
        string PackageId) : IRequest<ErrorOr<bool>>;
}
