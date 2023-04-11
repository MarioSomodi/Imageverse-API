using Imageverse.Domain.Models;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate.Entites;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate
{
    public sealed class User : AggregateRoot<UserId>
    {
        private readonly List<PostId> _postIds = new();
        private readonly List<UserActionLog> _userActionLogs = new();
        private readonly List<UserLimit> _userLimits = new();

        public string Username { get; }
        public string Name { get; } 
        public string Surname { get; } 
        public string Email { get; } 
        public string ProfileImage { get; } 
        public string Password { get; } 
        public Package Package { get; }

        public IReadOnlyList<PostId> PostIds => _postIds.AsReadOnly();
        public IReadOnlyList<UserActionLog> UserActionLogs => _userActionLogs.AsReadOnly();
        public IReadOnlyList<UserLimit> UserLimits => _userLimits.AsReadOnly();

        private User(
            UserId userId,
            string username,
            string name,
            string surname,
            string email,
            string profileImage,
            string password,
            Package package)
            : base(userId)
        {
            Username = username;
            Name = name;
            Surname = surname;
            Email = email;
            ProfileImage = profileImage;
            Password = password;
            Package = package;
        }

        public static User Create(
            string username,
            string name,
            string surname,
            string email,
            string profileImage,
            string password,
            Package package)
        {
            return new(
                UserId.CreateUnique(),
                username,
                name,
                surname,
                email,
                profileImage,
                password,
                package);
        }
    }
}
