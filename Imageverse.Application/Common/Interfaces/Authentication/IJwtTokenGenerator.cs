using ErrorOr;
using Imageverse.Domain.Common;
using Imageverse.Domain.UserAggregate;
using System.Security.Claims;

namespace Imageverse.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        RefreshTokenResult GenerateRefreshToken();
        ErrorOr<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    }
}
