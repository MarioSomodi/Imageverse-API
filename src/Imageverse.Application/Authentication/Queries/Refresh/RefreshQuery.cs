using ErrorOr;
using MediatR;

namespace Imageverse.Application.Authentication.Queries.Refresh
{
    public record RefreshQuery(string ExpiredAccessToken, string RefreshToken) : IRequest<ErrorOr<string>>;
}
