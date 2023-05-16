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
using Imageverse.Application.Common.Interfaces;

namespace Imageverse.Application.Users.Commands.UserPackageChange
{
    public class UserPackageChangeCommandHandler : IRequestHandler<UserPackageChangeCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _mediator;

        public UserPackageChangeCommandHandler(IPublisher mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(UserPackageChangeCommand request, CancellationToken cancellationToken)
        {
            bool packageCanBeChanged = true;
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
            if (!Guid.TryParse(request.Id, out var id) || !Guid.TryParse(request.PackageId, out var packageId))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(UserId.Create(id)) is not User userToUpdate)
            {
                return Errors.Common.NotFound(nameof(User));
            }
            PackageId currentPackageId = userToUpdate.PackageId;
            if (await _unitOfWork.GetRepository<IPackageRepository>().FindByIdAsync(PackageId.Create(packageId)) is not Package packageToChangeTo)
            {
                return Errors.Common.NotFound(nameof(Package));
            }
            if (userToUpdate.PackageId.Equals(packageToChangeTo.Id))
            {
                return Errors.User.PackageConflict;
            }
            if (userToUpdate.UserLimitIds.Count > 0)
            {
                List<UserLimit> userLimits = (List<UserLimit>)await _unitOfWork.GetRepository<IUserLimitRepository>().FindAllById(userToUpdate.UserLimitIds);
                UserLimit? userLimit = userLimits.Where(uL => DateOnly.FromDateTime(uL.Date) == currentDate).FirstOrDefault();
                if(userLimit is not null && userLimit.RequestedChangeOfPackage)
                    packageCanBeChanged = false;
            }
            if(packageCanBeChanged)
            {
                userToUpdate.UpdatePreviousPackageId(userToUpdate, userToUpdate.PackageId);
                userToUpdate.UpdatePackageId(userToUpdate, packageToChangeTo.Id);
                userToUpdate.UpdatePackageValidFrom(userToUpdate, DateTime.UtcNow.AddDays(1));
                _unitOfWork.GetRepository<IUserRepository>().Update(userToUpdate);
                bool success = await _unitOfWork.CommitAsync();
                if (success) await _mediator.Publish(new UserChangedPackage(userToUpdate.Id, currentPackageId, packageToChangeTo));
                return success;
            }
            return Errors.Common.MethodNotAllowed("Cannot change package more than once a day.");
        }
    }
}
