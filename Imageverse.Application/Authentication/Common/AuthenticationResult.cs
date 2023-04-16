using Imageverse.Domain.UserAggregate;

namespace Imageverse.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}