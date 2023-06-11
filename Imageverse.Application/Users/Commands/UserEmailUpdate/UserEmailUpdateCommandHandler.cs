using ErrorOr;
using Imageverse.Application.Common.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatabaseLogger _databaseLogger;

        public UserEmailUpdateCommandHandler(IDatabaseLogger databaseLogger, IUnitOfWork unitOfWork)
        {
            _databaseLogger = databaseLogger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(UserEmailUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(UserId.Create(id)) is not User userToUpdate)
            {
                return Errors.Common.NotFound(nameof(User));
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().GetSingleOrDefaultAsync(u => u.Email == request.Email && u.AuthenticationType == request.authenticationType) is User userWithSameEmail)
            {
                return Errors.User.DuplicateEmail;
            }
            userToUpdate.UpdateEmail(userToUpdate, request.Email);

            _unitOfWork.GetRepository<IUserRepository>().Update(userToUpdate);
            bool success = await _unitOfWork.CommitAsync();
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
