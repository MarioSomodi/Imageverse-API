using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserAggregate.Events
{
    public record UserLoggedIn(User user) : IDomainEvent;
}
