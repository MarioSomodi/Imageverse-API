using Imageverse.Contracts.User;

namespace Imageverse.Contracts.Authentication
{
    public record AuthenticationResponse(
        UserResponse User,
        string Token);
}
