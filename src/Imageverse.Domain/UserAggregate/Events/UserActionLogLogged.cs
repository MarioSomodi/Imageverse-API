using Imageverse.Domain.Models;
using Imageverse.Domain.UserActionLogAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate.Events
{
    public record UserActionLogLogged(UserActionLogId userActionLogId, UserId userId) : IDomainEvent;
}
