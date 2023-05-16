using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Events
{
    public class UserActionLogLoggedEventHandler : INotificationHandler<UserActionLogLogged>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserActionLogLoggedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserActionLogLogged notification, CancellationToken cancellationToken)
        {
            User? user = await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(notification.userId);
            user!.AddUserActionLogId(user, notification.userActionLogId);
            _unitOfWork.GetRepository<IUserRepository>().Update(user);
            await _unitOfWork.CommitAsync();
        }
    }
}
