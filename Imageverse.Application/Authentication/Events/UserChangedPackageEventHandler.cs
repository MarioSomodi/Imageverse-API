using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Events
{
    public class UserChangedPackageEventHandler : INotificationHandler<UserChangedPackage>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IUserLimitRepository _userLimitRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDatabaseLogger _databaseLogger;

        public UserChangedPackageEventHandler(IPackageRepository packageRepository, IUserLimitRepository userLimitRepository, IDatabaseLogger databaseLogger, IUserRepository userRepository)
        {
            _packageRepository = packageRepository;
            _userLimitRepository = userLimitRepository;
            _databaseLogger = databaseLogger;
            _userRepository = userRepository;
        }

        public async Task Handle(UserChangedPackage notification, CancellationToken cancellationToken)
        {
            Package? package = await _packageRepository.GetByIdAsync(notification.oldPackage); 
            User? user = await _userRepository.GetByIdAsync(notification.UserId);


            //TODO logic for creating the user limit when package change is triggered should be done i a special method inside the userlimitrepo
            //TODO create event to trigger when user limit is added so its id is added to the user
        }
    }
}
