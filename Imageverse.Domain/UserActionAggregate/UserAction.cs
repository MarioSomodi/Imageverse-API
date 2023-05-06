using Imageverse.Domain.Models;
using Imageverse.Domain.UserActionAggregate.ValueObjects;

namespace Imageverse.Domain.UserActionAggregate
{
    public sealed class UserAction : AggregateRoot<UserActionId>
    {
        public int Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private UserAction(
            UserActionId userActionId,
            string name,
            string description)
            : base(userActionId)
        {
            Name = name;
            Description = description;
        }

        public static UserAction Create(string name, string description)
        {
            return new(
                UserActionId.CreateUnique(),
                name,
                description);
        }

#pragma warning disable CS8618
        private UserAction()
        {
        }
#pragma warning restore CS8618 
    }
}
