using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Imageverse.Domain.Common.AppErrors;
using MediatR;
using Imageverse.Domain.UserAggregate;

namespace Imageverse.Application.Users.Commands.UserIsAdminChange
{
    public class UserIsAdminChangeCommandHandler : IRequestHandler<UserIsAdminChangeCommand, ErrorOr<bool>>
    {
        private readonly IUserRepository _userRepository;
        public UserIsAdminChangeCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<bool>> Handle(UserIsAdminChangeCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _userRepository.GetByIdAsync(UserId.Create(id)) is not User userToMakeAdmin)
            {
                return Errors.Common.NotFound(nameof(User));
            }

            userToMakeAdmin.UpdateIsAdmin(userToMakeAdmin, request.IsAdmin);

            return await _userRepository.UpdateAsync(userToMakeAdmin);
        }
    }
}
