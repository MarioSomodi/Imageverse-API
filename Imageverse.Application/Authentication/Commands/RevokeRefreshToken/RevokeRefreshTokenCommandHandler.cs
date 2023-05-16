using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Authentication.Commands.RevokeRefreshToken
{
    internal class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RevokeRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(UserId.Create(id)) is not User userToRevokeRefreshToken)
            {
                return Errors.Common.NotFound(nameof(User));
            }
            userToRevokeRefreshToken.UpdateRefreshTokenExpiry(userToRevokeRefreshToken, DateTime.UtcNow.AddDays(-1));
            _unitOfWork.GetRepository<IUserRepository>().Update(userToRevokeRefreshToken);
            return await _unitOfWork.CommitAsync();
        }
    }
}
