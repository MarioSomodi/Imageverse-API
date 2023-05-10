using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Users.Commands.UserEmailUpdate
{
    public class UserEmailUpdateCommandHandler : IRequestHandler<UserEmailUpdateCommand, ErrorOr<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDatabaseLogger _databaseLogger;

        public UserEmailUpdateCommandHandler(IUserRepository userRepository, IDatabaseLogger databaseLogger)
        {
            _userRepository = userRepository;
            _databaseLogger = databaseLogger;
        }

        public async Task<ErrorOr<bool>> Handle(UserEmailUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _userRepository.GetByIdAsync(UserId.Create(id)) is not User userToUpdate)
            {
                return Errors.Common.NotFound(nameof(User));
            }
            if (await _userRepository.GetSingleOrDefaultByPropertyValueAsync(nameof(request.Email), request.Email) is User userWithSameEmail)
            {
                return Errors.User.DuplicateEmail;
            }
            userToUpdate.UpdateEmail(userToUpdate, request.Email);

            bool success = await _userRepository.UpdateAsync(userToUpdate);

            if (success)
            {
                await _databaseLogger.LogUserAction(UserActions.UserChangedEmail,
                                                    $"User {userToUpdate.Username} changed his email from {request.Email} to {userToUpdate.Email}.",
                                                    userToUpdate.Id);
            }
            
            return success;
        }
    }
}
