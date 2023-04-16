using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void Add(User user);
    }
}
