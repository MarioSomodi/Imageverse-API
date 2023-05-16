using ErrorOr;
using MediatR;

namespace Imageverse.Application.Authentication.Commands.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(string Id) : IRequest<ErrorOr<bool>>;
}
