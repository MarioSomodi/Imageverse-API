using ErrorOr;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;
using Imageverse.Domain.Common.Enums;

namespace Imageverse.Application.Users.Commands.UserPasswordUpdate
{
    public class UserPasswordUpdateCommandHandler : IRequestHandler<UserPasswordUpdateCommand, ErrorOr<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDatabaseLogger _databaseLogger;
        private readonly IPasswordHasher _passwordHasher;

        public UserPasswordUpdateCommandHandler(IDatabaseLogger databaseLogger, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _databaseLogger = databaseLogger;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<bool>> Handle(UserPasswordUpdateCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdAsync(UserId.Create(new Guid(request.Id))) is not User userToUpdate
                || !_passwordHasher.VerifyPassword(request.CurrentPassword, userToUpdate.Password, userToUpdate.Salt))
            {
                return Errors.Authentication.InvalidPassword;
            }

            var hashedNewPassword = _passwordHasher.HashPassword(request.NewPassword, out byte[] newSalt);

            userToUpdate.UpdatePassword(userToUpdate, hashedNewPassword);
            userToUpdate.UpdateSalt(userToUpdate, newSalt);

            bool success = await _userRepository.UpdateAsync(userToUpdate);
            if (success)
            {
                await _databaseLogger.LogUserAction(
                    UserActions.UserChangedPassword,
                    $"User with email {userToUpdate.Email} and Id: {userToUpdate.Id} updated his password.",
                    userToUpdate);
            }
            return success;
        }
    }
}
