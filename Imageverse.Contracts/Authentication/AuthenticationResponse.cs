using Imageverse.Contracts.Packages;

namespace Imageverse.Contracts.Authentication
{
    public record AuthenticationResponse(
        string Id,
        string Username,
        string Name,
        string Surname,
        string Email,
        string ProfileImage,
        PackageResponse Package,
        List<string> PostIds,
        List<string> UserActionLogs,
        List<string> UserLimits,
        string Token);
}
