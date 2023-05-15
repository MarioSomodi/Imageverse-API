using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Domain.UserLimitAggregate.Events;
using MediatR;

namespace Imageverse.Application.Users.Events
{
    public class UserChangedPackageEventHandler : INotificationHandler<UserChangedPackage>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatabaseLogger _databaseLogger;
        private readonly IPublisher _mediator;

        public UserChangedPackageEventHandler(IDatabaseLogger databaseLogger, IPublisher mediator, IUnitOfWork unitOfWork)
        {
            _databaseLogger = databaseLogger;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserChangedPackage notification, CancellationToken cancellationToken)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

            Package? oldPackage = await _unitOfWork.GetRepository<IPackageRepository>().FindById(notification.oldPackage);
            User? user = await _unitOfWork.GetRepository<IUserRepository>().FindById(notification.UserId);
            await _databaseLogger.LogUserAction(UserActions.UserChangedPackage, $"User changed their package from {oldPackage!.Name} to {notification.PackageChangedTo.Name}", user!.Id);
            if (_unitOfWork.GetRepository<IUserLimitRepository>().GetUserLimitIfExistsForDate(currentDate, user!.UserLimitIds.ToList()) is UserLimit userLimitOnDate)
            {
                userLimitOnDate.UpdateRequestedChangeOfPackage(userLimitOnDate, true);
                _unitOfWork.GetRepository<IUserLimitRepository>().Update(userLimitOnDate);
                return;
            }
            UserLimit userLimit = UserLimit.Create(0, 0, true);
            await _unitOfWork.GetRepository<IUserLimitRepository>().AddAsync(userLimit);
            bool success = await _unitOfWork.CommitAsync();
            if (success)
            {
                await _databaseLogger.LogUserAction(UserActions.NewUserLimitAdded, "User had a new limit connected to him added because he triggered an action that is limited by his package", user.Id);
                await _mediator.Publish(new UserLimitAdded(user, userLimit.Id));
            }
        }
    }
}
