using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserLimitAggregate.Events;
using MediatR;

namespace Imageverse.Application.Users.Events
{
    public class UserLimitAddedEventHandler : INotificationHandler<UserLimitAdded>
    {
        private readonly IUserRepository _userRepository;

        public UserLimitAddedEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserLimitAdded notification, CancellationToken cancellationToken)
        {
            notification.User.AddUserLimitId(notification.User, notification.userLimitId);
            await _userRepository.UpdateAsync(notification.User);
        }
    }
}
