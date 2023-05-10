using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Events
{
    public class UserRegisteredEventHandler : INotificationHandler<UserRegistered>
    {
        private readonly IDatabaseLogger _databaseLogger;

        public UserRegisteredEventHandler(IDatabaseLogger databaseLogger)
        {
            _databaseLogger = databaseLogger;
        }

        public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
        {
            var user = notification.user;
            await _databaseLogger.LogUserAction(UserActions.UserRegistered, 
                $"User {user.Name} {user.Surname} with the username {user.Username} and email {user.Email} has registered succesfully.",
                user);
        }
    }
}
