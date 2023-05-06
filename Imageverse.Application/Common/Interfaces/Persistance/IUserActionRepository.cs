using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserActionAggregate.ValueObjects;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IUserActionRepository : IRepository<UserAction, UserActionId>
    {
    }
}
