using ErrorOr;
using MediatR;

namespace Imageverse.Application.Users.Commands.UserPasswordUpdate
{
    public record UserPasswordUpdateCommand(
        string Id, 
        string CurrentPassword, 
        string NewPassword) : IRequest<ErrorOr<bool>>;
}
