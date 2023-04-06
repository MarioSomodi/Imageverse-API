using Imageverse.Domain.User;

namespace Imageverse.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}
