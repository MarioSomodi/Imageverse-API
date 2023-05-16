using ErrorOr;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;
using Imageverse.Domain.Common.Enums;
using Imageverse.Application.Common.Interfaces;

namespace Imageverse.Application.Users.Commands.UserPasswordUpdate
{
    public class UserPasswordUpdateCommandHandler : IRequestHandler<UserPasswordUpdateCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatabaseLogger _databaseLogger;
        private readonly IPasswordHasher _passwordHasher;

        public UserPasswordUpdateCommandHandler(IDatabaseLogger databaseLogger, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _databaseLogger = databaseLogger;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(UserPasswordUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(UserId.Create(id)) is not User userToUpdate
                || !_passwordHasher.VerifyPassword(request.CurrentPassword, userToUpdate.Password, userToUpdate.Salt))
            {
                return Errors.Authentication.InvalidPassword;
            }

            var hashedNewPassword = _passwordHasher.HashPassword(request.NewPassword, out byte[] newSalt);

            userToUpdate.UpdatePassword(userToUpdate, hashedNewPassword);
            userToUpdate.UpdateSalt(userToUpdate, newSalt);

            _unitOfWork.GetRepository<IUserRepository>().Update(userToUpdate);
            bool success = await _unitOfWork.CommitAsync();
            if (success)
            {
                await _databaseLogger.LogUserAction(
                    UserActions.UserChangedPassword,
                    $"User with email {userToUpdate.Email} updated his password.",
                    userToUpdate.Id);
            }
            return success;
        }
    }
}
