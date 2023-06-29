using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Application.Common.Interfaces.Services
{
    public interface IDatabaseLogger
    {
        public Task LogUserAction(UserActions userAction, string message, UserId user);
    }
}
