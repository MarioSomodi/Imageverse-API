using Imageverse.Domain.Models;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserLimitAggregate.ValueObjects;

namespace Imageverse.Domain.UserLimitAggregate.Events
{
    public record UserLimitAdded(User User, UserLimitId userLimitId) : IDomainEvent;
}
