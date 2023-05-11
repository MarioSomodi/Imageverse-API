using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Domain.UserLimitAggregate.Events;
using MediatR;

namespace Imageverse.Application.Users.Events
{
    public class UserChangedPackageEventHandler : INotificationHandler<UserChangedPackage>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IUserLimitRepository _userLimitRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDatabaseLogger _databaseLogger;
        private readonly IPublisher _mediator;

        public UserChangedPackageEventHandler(IPackageRepository packageRepository, IUserLimitRepository userLimitRepository, IDatabaseLogger databaseLogger, IUserRepository userRepository, IPublisher mediator)
        {
            _packageRepository = packageRepository;
            _userLimitRepository = userLimitRepository;
            _databaseLogger = databaseLogger;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task Handle(UserChangedPackage notification, CancellationToken cancellationToken)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

            Package? oldPackage = await _packageRepository.GetByIdAsync(notification.oldPackage);
            User? user = await _userRepository.GetByIdAsync(notification.UserId);
            await _databaseLogger.LogUserAction(UserActions.UserChangedPackage, $"User changed their package from {oldPackage!.Name} to {notification.PackageChangedTo.Name}", user!.Id);
            if (_userLimitRepository.GetUserLimitIfExistsForDate(currentDate, user!.UserLimitIds.ToList()) is UserLimit userLimitOnDate)
            {
                userLimitOnDate.UpdateRequestedChangeOfPackage(userLimitOnDate, true);
                await _userLimitRepository.UpdateAsync(userLimitOnDate);
                return;
            }
            UserLimit userLimit = UserLimit.Create(0, 0, true);
            bool success = await _userLimitRepository.AddAsync(userLimit);
            if(success)
            {
                await _databaseLogger.LogUserAction(UserActions.NewUserLimitAdded, "User had a new limit connected to him added because he triggered an action that is limited by his package", user.Id);
                await _mediator.Publish(new UserLimitAdded(user, userLimit.Id));
            }
        }
    }
}
