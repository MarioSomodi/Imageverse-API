using Imageverse.Domain.Entities;

namespace Imageverse.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}
