using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.Errors;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Users.Commands.Update
{
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;

        public UserUpdateCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<User>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _userRepository.GetByIdAsync(UserId.Create(new Guid(request.Id))) is not User userToUpdate)
            {
                return Errors.Common.NotFound(nameof(User));
            }

            if (!string.IsNullOrWhiteSpace(request.Username)) userToUpdate.UpdateUsername(userToUpdate, request.Username!);
            if (!string.IsNullOrWhiteSpace(request.Name)) userToUpdate.UpdateName(userToUpdate, request.Name!);
            if (!string.IsNullOrWhiteSpace(request.Surname)) userToUpdate.UpdateSurname(userToUpdate, request.Surname!);

            await _userRepository.UpdateAsync(userToUpdate);

            return userToUpdate;
        }
    }
}
