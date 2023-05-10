using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Domain.UserLimitAggregate.ValueObjects;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class UserLimitRepository : Repository<UserLimit, UserLimitId>, IUserLimitRepository
    {
        public UserLimitRepository(ImageverseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
