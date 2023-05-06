using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserActionAggregate.ValueObjects;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class UserActionRepository : Repository<UserAction, UserActionId>, IUserActionRepository
    {
        public UserActionRepository(ImageverseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
