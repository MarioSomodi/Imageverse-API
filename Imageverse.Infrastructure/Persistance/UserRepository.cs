using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.User;

namespace Imageverse.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        public int Add(User user)
        {
            user.Id = _users.Count;
            _users.Add(user);
            return _users.IndexOf(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email); 
        }
    }
}
