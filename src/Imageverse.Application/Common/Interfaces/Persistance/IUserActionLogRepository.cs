using Imageverse.Domain.UserActionLogAggregate;
using Imageverse.Domain.UserActionLogAggregate.ValueObjects;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IUserActionLogRepository : IRepository<UserActionLog, UserActionLogId>
    {
    }
}
