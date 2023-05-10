using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserAggregate;

namespace Imageverse.Application.Common.Interfaces.Services
{
    public interface IDatabaseLogger
    {
        public Task LogUserAction(UserActions userAction, string message, User user);
    }
}
