using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.UserAggregate.Events;

namespace Imageverse.Application.Users.Commands.UserPackageChange
{
    public class UserPackageChangeCommandHandler : IRequestHandler<UserPackageChangeCommand, ErrorOr<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IUserLimitRepository _userLimitRepository;
        private readonly IPublisher _mediator;

        public UserPackageChangeCommandHandler(IPublisher mediator, IPackageRepository packageRepository, IUserRepository userRepository, IUserLimitRepository userLimitRepository)
        {
            _mediator = mediator;
            _packageRepository = packageRepository;
            _userRepository = userRepository;
            _userLimitRepository = userLimitRepository;
        }

        public async Task<ErrorOr<bool>> Handle(UserPackageChangeCommand request, CancellationToken cancellationToken)
        {
            bool packageCanBeChanged = true;
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
            if (!Guid.TryParse(request.Id, out var id) || !Guid.TryParse(request.PackageId, out var packageId))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _userRepository.GetByIdAsync(UserId.Create(id)) is not User userToUpdate)
            {
                return Errors.Common.NotFound(nameof(User));
            }
            PackageId currentPackageId = userToUpdate.PackageId;
            if (await _packageRepository.GetByIdAsync(PackageId.Create(packageId)) is not Package packageToChangeTo)
            {
                return Errors.Common.NotFound(nameof(Package));
            }
            if (userToUpdate.PackageId.Equals(packageToChangeTo.Id))
            {
                return Errors.User.PackageConflict;
            }
            if (userToUpdate.UserLimitIds.Count > 0)
            {
                List<UserLimit> userLimits = (List<UserLimit>)await _userLimitRepository.GetMultipleByIds(userToUpdate.UserLimitIds);
                UserLimit? userLimit = userLimits.Where(uL => DateOnly.FromDateTime(uL.Date) == currentDate).FirstOrDefault();
                if(userLimit is not null && userLimit.RequestedChangeOfPackage)
                    packageCanBeChanged = false;
            }
            if(packageCanBeChanged)
            {
                userToUpdate.UpdatePackageId(userToUpdate, packageToChangeTo.Id);
                bool success = await _userRepository.UpdateAsync(userToUpdate);
                if(success) await _mediator.Publish(new UserChangedPackage(userToUpdate.Id, currentPackageId, packageToChangeTo));
                return success;
            }
            return Errors.Common.MethodNotAllowed("Cannot change package more than once a day.");
        }
    }
}
