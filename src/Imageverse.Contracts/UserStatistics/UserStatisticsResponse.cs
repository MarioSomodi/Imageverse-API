namespace Imageverse.Contracts.UserStatistics
{
    public record UserStatisticsResponse(
        string Id,
        double TotalMbUploaded,
        int TotalImagesUploaded,
        int TotalTimesUserRequestedPackageChange,
        DateTime FirstLogin,
        DateTime LastLogin,
        int TotalTimesLoggedIn,
        int TotalTimesPostsWereEdited);
}
