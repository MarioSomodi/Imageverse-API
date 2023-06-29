using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserLimitAggregate.Events;
using MediatR;

namespace Imageverse.Application.Users.Events
{
    public class UserLimitAddedEventHandler : INotificationHandler<UserLimitAdded>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserLimitAddedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserLimitAdded notification, CancellationToken cancellationToken)
        {
            notification.User.AddUserLimitId(notification.User, notification.userLimitId);
            _unitOfWork.GetRepository<IUserRepository>().Update(notification.User);
            await _unitOfWork.CommitAsync();
        }
    }
}
