using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Events
{
    public class UserActionLogLoggedHandler : INotificationHandler<UserActionLogLogged>
    {
        private readonly IUserRepository _userRepository;

        public UserActionLogLoggedHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserActionLogLogged notification, CancellationToken cancellationToken)
        {
            notification.user.AddUserActionLogId(notification.user, notification.userActionLogId);
            await _userRepository.UpdateAsync(notification.user);
        }
    }
}
