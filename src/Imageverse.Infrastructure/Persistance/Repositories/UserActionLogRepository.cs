using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserActionLogAggregate;
using Imageverse.Domain.UserActionLogAggregate.ValueObjects;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class UserActionLogRepository : Repository<UserActionLog, UserActionLogId>, IUserActionLogRepository
    {
        public UserActionLogRepository(ImageverseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
