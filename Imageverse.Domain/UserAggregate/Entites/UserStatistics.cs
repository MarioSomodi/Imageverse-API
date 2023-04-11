using Imageverse.Domain.Models;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate.Entites
{
    public sealed class UserStatistics : Entity<UserStatisticsId>
    {
        //In MB
        public int TotalUploaded { get; }
        public int TotalImagesUploaded { get; }
        public int TotalTimesUserRequestedPackageChange { get; set; }
        public DateTime FirstLogin { get; }
        public DateTime LastLogin { get; }
        public int TotalTimesLoggedIn { get; }
        public int TotalTimesPostsWereEdited { get; }


        private UserStatistics(
            UserStatisticsId userStatisticsId,
            int totalUploaded,
            int totalImagesUploaded,
            int totalTimesUserRequestedPackageChange,
            DateTime lastLogin,
            DateTime firstLogin,
            int totalTimesLoggedIn,
            int totalTimesPostsWereEdited)
            : base(userStatisticsId)
        {
            TotalUploaded = totalUploaded;
            TotalImagesUploaded = totalImagesUploaded;
            TotalTimesUserRequestedPackageChange = totalTimesUserRequestedPackageChange;
            FirstLogin = firstLogin;
            LastLogin = lastLogin;
            TotalTimesLoggedIn = totalTimesLoggedIn;
            TotalTimesPostsWereEdited = totalTimesPostsWereEdited;
        }

        public static UserStatistics Create(
            int totalUploaded,
            int totalImagesUploaded,
            int timesUserRequestedPackageChange,
            int timesLoggedIn, 
            int totalTimesPostsWereEdited)
        {
            return new(
                UserStatisticsId.CreateUnique(),
                totalUploaded,
                totalImagesUploaded,
                timesUserRequestedPackageChange,
                DateTime.UtcNow,
                DateTime.UtcNow,
                timesLoggedIn,
                totalTimesPostsWereEdited);
        }
    }
}
