using Imageverse.Domain.Models;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate.Entities
{
    public sealed class UserStatistics : Entity<UserStatisticsId>
    {
        public double TotalMBUploaded { get; private set; }
        public int TotalImagesUploaded { get; private set; }
        public int TotalTimesUserRequestedPackageChange { get; private set; }
        public DateTime FirstLogin { get; private set; }
        public DateTime LastLogin { get; private set; }
        public int TotalTimesLoggedIn { get; private set; }
        public int TotalTimesPostsWereEdited { get; private set; }

        private UserStatistics(
            UserStatisticsId userStatisticsId,
            double totalMBUploaded,
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
            double totalMBUploaded,
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
        public UserStatistics UpdateTotalMBUploaded(UserStatistics userStatisticsToUpdate, double totalMBUploaded)
        {
            userStatisticsToUpdate.TotalMBUploaded = totalMBUploaded;
            return userStatisticsToUpdate;
        }
        public UserStatistics UpdateTotalImagesUploaded(UserStatistics userStatisticsToUpdate, int totalImagesUploaded)
        {
            userStatisticsToUpdate.TotalImagesUploaded = totalImagesUploaded;
            return userStatisticsToUpdate;
        }
        public UserStatistics UpdateTotalTimesUserRequestedPackageChange(UserStatistics userStatisticsToUpdate, int totalTimesUserRequestedPackageChange)
        {
            userStatisticsToUpdate.TotalTimesUserRequestedPackageChange = totalTimesUserRequestedPackageChange;
            return userStatisticsToUpdate;
        }
        public UserStatistics UpdateLastLogin(UserStatistics userStatisticsToUpdate)
        {
            userStatisticsToUpdate.LastLogin = DateTime.UtcNow;
            return userStatisticsToUpdate;
        }
        public UserStatistics UpdateTotalTimesLoggedIn(UserStatistics userStatisticsToUpdate, int totalTimesLoggedIn)
        {
            userStatisticsToUpdate.TotalTimesLoggedIn = totalTimesLoggedIn;
            return userStatisticsToUpdate;
        }
        public UserStatistics UpdateTotalTimesPostsWereEdited(UserStatistics userStatisticsToUpdate, int totalTimesPostsWereEdited)
        {
            userStatisticsToUpdate.TotalTimesPostsWereEdited = totalTimesPostsWereEdited;
            return userStatisticsToUpdate;
        }

#pragma warning disable CS8618
        private UserStatistics()
        {
        }
#pragma warning restore CS8618 
    }
}
