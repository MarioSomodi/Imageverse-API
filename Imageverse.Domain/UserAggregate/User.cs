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
        public PackageId PreviousPackageId { get; private set; }
        public DateTime PackageValidFrom { get; private set; }
        public UserStatistics UserStatistics { get; private set; }
        public byte[] Salt { get; private set; }

        public bool IsAdmin { get; private set; }

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
            byte[] salt,
            PackageId previousPackageId,
            DateTime packageValidFrom,
            bool isAdmin)
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
            PreviousPackageId = previousPackageId;
            PackageValidFrom = packageValidFrom;
            IsAdmin = isAdmin;
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
                salt,
                packageId,
                DateTime.UtcNow,
                false);

            return user;
        }

        public PackageId GetUsersActivePackage(User user)
        {
            if (DateOnly.FromDateTime(DateTime.UtcNow) < DateOnly.FromDateTime(user.PackageValidFrom))
            {
                return user.PreviousPackageId;
            }
            return user.PackageId;
        }

        public User UpdateUsername(User userToUpdate, string username)
        {
            userToUpdate.Username = username;
            return userToUpdate;
        }

        public User UpdateName(User userToUpdate, string name)
        {
            userToUpdate.Name = name;
            return userToUpdate;
        }

        public User UpdateSurname(User userToUpdate, string surname)
        {
            userToUpdate.Surname = surname;
            return userToUpdate;
        }

        public User UpdateEmail(User userToUpdate, string email)
        {
            userToUpdate.Email = email;
            return userToUpdate;
        }
        public User UpdateProfileImage(User userToUpdate, string profileImage)
        {
            userToUpdate.ProfileImage = profileImage;
            return userToUpdate;
        }
        public User UpdatePassword(User userToUpdate, string password)
        {
            userToUpdate.Password = password;
            return userToUpdate;
        }
        public User UpdatePackageId(User userToUpdate, PackageId packageId)
        {
            userToUpdate.PackageId = packageId;
            return userToUpdate;
        }
        public User UpdateUserStatistics(User userToUpdate, UserStatistics userStatstics)
        {
            userToUpdate.UserStatistics = userStatstics;
            return userToUpdate;
        }
        public User UpdateSalt(User userToUpdate, byte[] salt)
        {
            userToUpdate.Salt = salt;
            return userToUpdate;
        }

        public User UpdatePreviousPackageId(User userToUpdate, PackageId previousPackageId)
        {
            userToUpdate.PreviousPackageId = previousPackageId;
            return userToUpdate;
        }

        public User UpdatePackageValidFrom(User userToUpdate, DateTime packageValidFrom)
        {
            userToUpdate.PackageValidFrom = packageValidFrom;
            return userToUpdate;
        }

        public User AddUserActionLogId(User userToUpdate, UserActionLogId userActionLogId)
        {
            userToUpdate._userActionLogIds.Add(userActionLogId);
            return userToUpdate;
        }

        public User AddPostId(User userToUpdate, PostId postId)
        {
            userToUpdate._postIds.Add(postId);
            return userToUpdate;
        }
        
        public User AddUserLimitId(User userToUpdate, UserLimitId userLimitId)
        {
            userToUpdate._userLimitIds.Add(userLimitId);
            return userToUpdate;
        }

        public User UpdateIsAdmin(User userToUpdate, bool isAdmin)
        {
            userToUpdate.IsAdmin = isAdmin;
            return userToUpdate;
        }

#pragma warning disable CS8618
        private User()
        {
        }
#pragma warning restore CS8618 
    }
}
