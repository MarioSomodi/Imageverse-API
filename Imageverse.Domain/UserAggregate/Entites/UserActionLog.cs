using Imageverse.Domain.Models;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate.Entites
{
    public sealed class UserActionLog : Entity<UserActionLogId>
    {
        public UserAction Action { get; }
        public DateTime Date { get; }
        public string Message { get; }

        private UserActionLog(
            UserActionLogId userActionLogId,
            UserAction action,
            DateTime date,
            string message)
            : base(userActionLogId) 
        {
            Action = action;
            Date = date;
            Message = message;
        }

        public static UserActionLog Create(UserAction action, string message)
        {
            return new(
                UserActionLogId.CreateUnique(),
                action,
                DateTime.UtcNow,
                message);
        }
    }
}
