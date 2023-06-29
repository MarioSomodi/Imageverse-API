using ErrorOr;
using MediatR;

namespace Imageverse.Application.Users.Commands.UserEmailUpdate
{
    public record UserEmailUpdateCommand(
        string Id,
        string Email,
        int authenticationType) : IRequest<ErrorOr<bool>>;
}
