using Imageverse.Domain.Models;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate.Events
{
    public record UserChangedPackage(UserId UserId, PackageId oldPackage , Package PackageChangedTo) : IDomainEvent;
}
