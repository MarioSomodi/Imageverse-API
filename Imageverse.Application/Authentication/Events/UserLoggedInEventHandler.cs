using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Events
{
    public class UserLoggedInEventHandler : INotificationHandler<UserLoggedIn>
    {
        private readonly IDatabaseLogger _databaseLogger;
        private readonly IUnitOfWork _unitOfWork;

        public UserLoggedInEventHandler(IDatabaseLogger databaseLogger, IUnitOfWork unitOfWork)
        {
            _databaseLogger = databaseLogger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserLoggedIn notification, CancellationToken cancellationToken)
        {
            var user = notification.user;
            await _databaseLogger.LogUserAction(UserActions.UserLoggedIn,
                $"User {user.Name} {user.Surname} with the username {user.Username} and email {user.Email} has logged in succesfully.",
                user.Id);

            user.UserStatistics.UpdateLastLogin(user.UserStatistics)
                               .UpdateTotalTimesLoggedIn(user.UserStatistics, user.UserStatistics.TotalTimesLoggedIn + 1);
            _unitOfWork.GetRepository<IUserRepository>().Update(user);
            await _unitOfWork.CommitAsync();
            await _databaseLogger.LogUserAction(UserActions.UsersStatisticsUpdated,
                $"User with email {user.Email} had his statistics updated.",
                user.Id);
        }
    }
}
