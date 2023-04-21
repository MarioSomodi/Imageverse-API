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
        string PackageId,
        List<string> PostIds,
        List<string> UserActionLogIds,
        List<string> UserLimitIds,
        string Token);
}
