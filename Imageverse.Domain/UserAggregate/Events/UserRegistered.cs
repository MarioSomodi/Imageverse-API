using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserAggregate.Events
{
    public record UserRegistered(User user) : IDomainEvent;
}
