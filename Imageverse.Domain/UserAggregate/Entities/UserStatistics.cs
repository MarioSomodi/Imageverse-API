using Imageverse.Domain.Models;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate.Entities
{
    public sealed class UserStatistics : Entity<UserStatisticsId>
    {
        public int TotalMBUploaded { get; private set; }
        public int TotalImagesUploaded { get; private set; }
        public int TotalTimesUserRequestedPackageChange { get; private set; }
        public DateTime FirstLogin { get; private set; }
        public DateTime LastLogin { get; private set; }
        public int TotalTimesLoggedIn { get; private set; }
        public int TotalTimesPostsWereEdited { get; private set; }

        private UserStatistics(
            UserStatisticsId userStatisticsId,
            int totalMBUploaded,
            int totalImagesUploaded,
            int totalTimesUserRequestedPackageChange,
            DateTime lastLogin,
            DateTime firstLogin,
            int totalTimesLoggedIn,
            int totalTimesPostsWereEdited)
            : base(userStatisticsId)
        {
            TotalMBUploaded = totalMBUploaded;
            TotalImagesUploaded = totalImagesUploaded;
            TotalTimesUserRequestedPackageChange = totalTimesUserRequestedPackageChange;
            FirstLogin = firstLogin;
            LastLogin = lastLogin;
            TotalTimesLoggedIn = totalTimesLoggedIn;
            TotalTimesPostsWereEdited = totalTimesPostsWereEdited;
        }

        public static UserStatistics Create(
            int totalMBUploaded,
            int totalImagesUploaded,
            int timesUserRequestedPackageChange,
            int timesLoggedIn,
            int totalTimesPostsWereEdited)
        {
            return new(
                UserStatisticsId.CreateUnique(),
                totalMBUploaded,
                totalImagesUploaded,
                timesUserRequestedPackageChange,
                DateTime.UtcNow,
                DateTime.UtcNow,
                timesLoggedIn,
                totalTimesPostsWereEdited);
        }

#pragma warning disable CS8618
        private UserStatistics()
        {
        }
#pragma warning restore CS8618 
    }
}
