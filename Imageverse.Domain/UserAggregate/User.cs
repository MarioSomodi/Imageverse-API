using Imageverse.Domain.Models;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Imageverse.Domain.UserActionLogAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate.Entities;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Imageverse.Domain.UserLimitAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate
{
    public sealed class User : AggregateRoot<UserId>
    {
        private readonly List<PostId> _postIds = new();
        private readonly List<UserActionLogId> _userActionLogIds = new();
        private readonly List<UserLimitId> _userLimitIds = new();

        public string Username { get; private set; }
        public string Name { get; private set; } 
        public string Surname { get; private set; } 
        public string Email { get; private set; } 
        public string ProfileImage { get; private set; } 
        public string Password { get; private set; } 
        public PackageId PackageId { get; private set; }
        public UserStatistics UserStatistics { get; private set; }
        public byte[] Salt { get; private set; }

        public IReadOnlyList<PostId> PostIds => _postIds.AsReadOnly();
        public IReadOnlyList<UserActionLogId> UserActionLogIds => _userActionLogIds.AsReadOnly();
        public IReadOnlyList<UserLimitId> UserLimitIds => _userLimitIds.AsReadOnly();

        private User(
            UserId userId,
            string username,
            string name,
            string surname,
            string email,
            string profileImage,
            string password,
            PackageId packageId,
            UserStatistics userStatistics,
            byte[] salt)
            : base(userId)
        {
            Username = username;
            Name = name;
            Surname = surname;
            Email = email;
            ProfileImage = profileImage;
            Password = password;
            PackageId = packageId;
            UserStatistics = userStatistics;
            Salt = salt;
        }

        public static User Create(
            string username,
            string name,
            string surname,
            string email,
            string profileImage,
            string password,
            PackageId packageId,
            UserStatistics userStatstics,
            byte[] salt)
        {
            User user = new(
                UserId.CreateUnique(),
                username,
                name,
                surname,
                email,
                profileImage,
                password,
                packageId,
                userStatstics,
                salt);

            return user;
        }

#pragma warning disable CS8618
        private User()
        {
        }
#pragma warning restore CS8618 
    }
}
