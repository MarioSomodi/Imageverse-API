using Imageverse.Domain.Models;
using Imageverse.Domain.Post.ValueObjects;

namespace Imageverse.Domain.User.Entites
{
    public sealed class UserActionLog : Entity<UserActionLogId>
    {
        public UserAction Action { get; }
        public DateTime Date { get; }
        public string Message { get; }

        private UserActionLog(
            UserActionLogId userActionLogId,
            DateTime date,
            string message)
            : base(userActionLogId) 
        {
            Date = date;
            Message = message;
        }

        public static UserActionLog Create(string message)
        {
            return new(
                UserActionLogId.CreateUnique(),
                DateTime.UtcNow,
                message);
        }
    }
}
