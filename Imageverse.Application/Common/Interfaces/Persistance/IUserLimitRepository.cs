using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Domain.UserLimitAggregate.ValueObjects;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IUserLimitRepository : IRepository<UserLimit, UserLimitId>
    {
    }
}
