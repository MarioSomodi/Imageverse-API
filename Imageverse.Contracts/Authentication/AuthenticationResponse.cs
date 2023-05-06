using Imageverse.Contracts.Packages;
using Imageverse.Contracts.UserStatistics;

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
        UserStatisticsResponse UserStatistics,
        string Token);
}
