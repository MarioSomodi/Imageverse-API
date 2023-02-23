using Imageverse.Domain.Entities;

namespace Imageverse.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
