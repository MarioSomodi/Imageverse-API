using ErrorOr;
using MediatR;

namespace Imageverse.Application.Users.Commands.UserIsAdminChange
{
    public record UserIsAdminChangeCommand(string Id, bool IsAdmin) : IRequest<ErrorOr<bool>>;
}
