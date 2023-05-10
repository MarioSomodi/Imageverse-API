using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Events
{
    public class UserActionLogLoggedEventHandler : INotificationHandler<UserActionLogLogged>
    {
        private readonly IUserRepository _userRepository;

        public UserActionLogLoggedEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserActionLogLogged notification, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(notification.userId);
            user!.AddUserActionLogId(user, notification.userActionLogId);
            await _userRepository.UpdateAsync(user);
        }
    }
}
