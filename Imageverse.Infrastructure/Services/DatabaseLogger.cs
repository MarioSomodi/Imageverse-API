using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserActionLogAggregate;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Infrastructure.Services
{
    public class DatabaseLogger : IDatabaseLogger
    {
        private readonly IUserActionRepository _userActionRepository;
        private readonly IUserActionLogRepository _userActionLogRepository;
        private readonly IPublisher _publisher;

        public DatabaseLogger(IUserActionLogRepository userActionLogRepository, IUserActionRepository userActionRepository, IPublisher publisher)
        {
            _userActionLogRepository = userActionLogRepository;
            _userActionRepository = userActionRepository;
            _publisher = publisher;
        }

        public async Task LogUserAction(UserActions userAction, string message, User user)
        {
            UserAction? action = await _userActionRepository.GetSingleOrDefaultByPropertyValueAsync(nameof(UserAction.Code), (int)userAction);
            UserActionLog userActionLog = UserActionLog.Create(action!.Id, message);
            bool success = await _userActionLogRepository.AddAsync(userActionLog);
            if(success)
            {
                await _publisher.Publish(new UserActionLogLogged(userActionLog.Id, user));
            }
        }
    }
}
