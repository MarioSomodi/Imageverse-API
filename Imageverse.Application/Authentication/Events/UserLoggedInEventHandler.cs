using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Events
{
    public class UserLoggedInEventHandler : INotificationHandler<UserLoggedIn>
    {
        private readonly IDatabaseLogger _databaseLogger;

        public UserLoggedInEventHandler(IDatabaseLogger databaseLogger)
        {
            _databaseLogger = databaseLogger;
        }

        public async Task Handle(UserLoggedIn notification, CancellationToken cancellationToken)
        {
            var user = notification.user;
            await _databaseLogger.LogUserAction(UserActions.UserLoggedIn,
                $"User {user.Name} {user.Surname} with the username {user.Username} and email {user.Email} has logged in succesfully.");
        }
    }
}
