using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserActionLogAggregate;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Infrastructure.Services
{
    public class DatabaseLogger : IDatabaseLogger
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public DatabaseLogger(IPublisher publisher, IUnitOfWork unitOfWork)
        {
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task LogUserAction(UserActions userAction, string message, UserId userId)
        {
            UserAction? action = await _unitOfWork.GetRepository<IUserActionRepository>().GetSingleOrDefaultAsync(uA => uA.Code == (int)userAction);
            UserActionLog userActionLog = UserActionLog.Create(action!.Id, message + $"User id: {userId.Value}");
            await _unitOfWork.GetRepository<IUserActionLogRepository>().AddAsync(userActionLog);
            bool success = await _unitOfWork.CommitAsync();
            if(success)
            {
                await _publisher.Publish(new UserActionLogLogged(userActionLog.Id, userId));
            }
        }
    }
}
