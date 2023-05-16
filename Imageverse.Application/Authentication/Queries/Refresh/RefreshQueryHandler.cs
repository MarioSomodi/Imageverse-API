using ErrorOr;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;
using System.Security.Claims;

namespace Imageverse.Application.Authentication.Queries.Refresh
{
    public class RefreshQueryHandler : IRequestHandler<RefreshQuery, ErrorOr<string>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<string>> Handle(RefreshQuery request, CancellationToken cancellationToken)
        {
            ErrorOr<ClaimsPrincipal> expiredTokenClaimsPrincipal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);
            if (expiredTokenClaimsPrincipal.IsError)
            {
                return expiredTokenClaimsPrincipal.Errors.First();
            }
            if(!Guid.TryParse(expiredTokenClaimsPrincipal.Value.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id))
            {
                return Errors.Common.BadRequest("Sent expired access token has no valid claim for users name identifier");
            }
            UserId userId = UserId.Create(id);
            if(await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(userId) is not User userToRefreshAccessToken)
            {
                return Errors.Common.NotFound(nameof(User));
            }
            if(userToRefreshAccessToken.RefreshToken == request.RefreshToken
               && DateOnly.FromDateTime(userToRefreshAccessToken.RefreshTokenExpiry) >= DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return _jwtTokenGenerator.GenerateToken(userToRefreshAccessToken);
            }
            return Errors.Common.Unauthorized("The refresh token has expired. Please re-authenticate.");
        }
    }
}
