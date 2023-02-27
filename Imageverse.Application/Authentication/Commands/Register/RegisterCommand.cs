using ErrorOr;
using Imageverse.Application.Authentication.Common;
using MediatR;

namespace Imageverse.Application.Authentication.Commands.Register
{
    public record RegisterCommand(int PackageId,
        string Name,
        string Username,
        string Surname,
        string Email,
        string ProfileImage,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
