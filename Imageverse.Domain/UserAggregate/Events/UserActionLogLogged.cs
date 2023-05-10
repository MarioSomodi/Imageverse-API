using Imageverse.Domain.Models;
using Imageverse.Domain.UserActionLogAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate.Events
{
    public record UserActionLogLogged(UserActionLogId userActionLogId, User user) : IDomainEvent;
}
