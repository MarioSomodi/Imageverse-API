using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ImageverseDbContext _dbContext;

        public UserRepository(ImageverseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
