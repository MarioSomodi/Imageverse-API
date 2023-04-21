using Imageverse.Domain.Models;
using Imageverse.Domain.UserActionAggregate.ValueObjects;
using Imageverse.Domain.UserActionLogAggregate.ValueObjects;

namespace Imageverse.Domain.UserActionLogAggregate
{
    public sealed class UserActionLog : AggregateRoot<UserActionLogId>
    {
        public UserActionId ActionId { get; private set; }
        public DateTime Date { get; private set; }
        public string Message { get; private set; }

        private UserActionLog(
            UserActionLogId userActionLogId,
            UserActionId actionId,
            DateTime date,
            string message)
            : base(userActionLogId)
        {
            ActionId = actionId;
            Date = date;
            Message = message;
        }

        public static UserActionLog Create(UserActionId actionId, string message)
        {
            return new(
                UserActionLogId.CreateUnique(),
                actionId,
                DateTime.UtcNow,
                message);
        }

#pragma warning disable CS8618
        private UserActionLog()
        {
        }
#pragma warning restore CS8618 
    }
}
