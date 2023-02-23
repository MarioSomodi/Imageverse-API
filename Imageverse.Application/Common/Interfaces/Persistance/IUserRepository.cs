using Imageverse.Domain.Entities;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        int Add(User user);
    }
}
