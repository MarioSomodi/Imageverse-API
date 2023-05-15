using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Users.Commands.UserInfoUpdate
{
    public class UserInfoUpdateCommandHandler : IRequestHandler<UserInfoUpdateCommand, ErrorOr<User>>
    {
        private readonly IDatabaseLogger _databaseLogger;
        private readonly IUnitOfWork _unitOfWork;

        public UserInfoUpdateCommandHandler(IDatabaseLogger databaseLogger, IUnitOfWork unitOfWork)
        {
            _databaseLogger = databaseLogger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<User>> Handle(UserInfoUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindById(UserId.Create(id)) is not User userToUpdate)
            {
                return Errors.Common.NotFound(nameof(User));
            }

            string updatedProperties = "";

            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                userToUpdate.UpdateUsername(userToUpdate, request.Username!);
                updatedProperties += $"{nameof(request.Username)} ";
            }
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                userToUpdate.UpdateName(userToUpdate, request.Name!);
                updatedProperties += $"{nameof(request.Name)} ";

            }
            if (!string.IsNullOrWhiteSpace(request.Surname))
            {
                userToUpdate.UpdateSurname(userToUpdate, request.Surname!);
                updatedProperties += $"{nameof(request.Surname)} ";
            }

            _unitOfWork.GetRepository<IUserRepository>().Update(userToUpdate);

            bool success = await _unitOfWork.CommitAsync();

            if(success)
                await _databaseLogger.LogUserAction(
                    UserActions.UserInfoChanged,
                    $"User with email {userToUpdate.Email} updated his info ({updatedProperties}).",
                    userToUpdate.Id);

            return userToUpdate;
        }
    }
}
