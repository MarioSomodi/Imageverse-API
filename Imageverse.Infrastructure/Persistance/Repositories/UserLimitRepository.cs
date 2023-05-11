using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Domain.UserLimitAggregate.ValueObjects;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class UserLimitRepository : Repository<UserLimit, UserLimitId>, IUserLimitRepository
    {
        public UserLimitRepository(ImageverseDbContext dbContext) : base(dbContext)
        {
        }

        public UserLimit? GetUserLimitIfExistsForDate(DateOnly date, List<UserLimitId> userLimitIds)
        {
            UserLimit? userLimit = _entityDbSet.ToList().Where(uL => DateOnly.FromDateTime(uL.Date) == date && userLimitIds.Contains(uL.Id)).FirstOrDefault();
            if (userLimit is not null)
                return userLimit;
            return null;
        }
    }
}
