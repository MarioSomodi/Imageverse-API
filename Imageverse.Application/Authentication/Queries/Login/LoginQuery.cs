using ErrorOr;
using Imageverse.Application.Authentication.Common;
using MediatR;

namespace Imageverse.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
