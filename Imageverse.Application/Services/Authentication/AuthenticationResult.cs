using Imageverse.Domain.Entities;

namespace Imageverse.Application.Services.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token);
}
