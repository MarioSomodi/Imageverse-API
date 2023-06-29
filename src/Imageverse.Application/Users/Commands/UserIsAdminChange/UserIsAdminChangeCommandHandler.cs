using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Users.Commands.UserIsAdminChange
{
    public class UserIsAdminChangeCommandHandler : IRequestHandler<UserIsAdminChangeCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserIsAdminChangeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<bool>> Handle(UserIsAdminChangeCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(UserId.Create(id)) is not User userToMakeAdmin)
            {
                return Errors.Common.NotFound(nameof(User));
            }

            userToMakeAdmin.UpdateIsAdmin(userToMakeAdmin, request.IsAdmin);

            _unitOfWork.GetRepository<IUserRepository>().Update(userToMakeAdmin);
            return await _unitOfWork.CommitAsync();
        }
    }
}
