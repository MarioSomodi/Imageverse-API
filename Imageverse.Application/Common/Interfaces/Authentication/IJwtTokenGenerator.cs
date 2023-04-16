using Imageverse.Domain.UserAggregate;

namespace Imageverse.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
