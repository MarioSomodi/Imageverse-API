using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task Add(User user);
    }
}
