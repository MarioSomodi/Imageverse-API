using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserActionLogAggregate;

namespace Imageverse.Infrastructure.Services
{
    public class DatabaseLogger : IDatabaseLogger
    {
        private readonly IUserActionRepository _userActionRepository;
        private readonly IUserActionLogRepository _userActionLogRepository;

        public DatabaseLogger(IUserActionLogRepository userActionLogRepository, IUserActionRepository userActionRepository)
        {
            _userActionLogRepository = userActionLogRepository;
            _userActionRepository = userActionRepository;
        }

        public async Task LogUserAction(UserActions userAction, string message)
        {
            UserAction? action = await _userActionRepository.GetSingleOrDefaultByPropertyValueAsync(nameof(UserAction.Code), (int)userAction);
            UserActionLog userActionLog = UserActionLog.Create(action!.Id, message);
            await _userActionLogRepository.AddAsync(userActionLog);
            await _userActionLogRepository.SaveChangesAsync();
        }
    }
}
