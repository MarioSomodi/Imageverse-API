using ErrorOr;
using MediatR;

namespace Imageverse.Application.Authentication.Queries.UserExists
{
    public record UserExistsQuery(
        string AuthenticationProviderId
        ) : IRequest<ErrorOr<bool>>;
}
