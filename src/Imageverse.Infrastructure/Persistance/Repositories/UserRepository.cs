using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ImageverseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
