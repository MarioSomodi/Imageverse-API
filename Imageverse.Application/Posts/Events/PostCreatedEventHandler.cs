using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.PostAggregate.Events;
using Imageverse.Domain.UserLimitAggregate;
using MediatR;

namespace Imageverse.Application.Posts.Events
{
    internal class PostCreatedEventHandler : INotificationHandler<PostCreated>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IDatabaseLogger _databaseLogger;


        public PostCreatedEventHandler(IUnitOfWork unitOfWork, IDatabaseLogger databaseLogger)
        {
            _unitOfWork = unitOfWork;
            _databaseLogger = databaseLogger;
        }

        public async Task Handle(PostCreated notification, CancellationToken cancellationToken)
        {
            var user = notification.User;
            var post = notification.Post;

            post.AddHashtagIds(post, notification.Hashtags.ConvertAll(h => h.Id));

            UserLimit? userLimitToday = _unitOfWork.GetRepository<IUserLimitRepository>().GetUserLimitIfExistsForDate(DateOnly.FromDateTime(DateTime.UtcNow), user.UserLimitIds.ToList());

            if(userLimitToday is null)
            {
                UserLimit userLimit = UserLimit.Create(
                    notification.ImageSize,
                    1,
                    false);
                await _unitOfWork.GetRepository<IUserLimitRepository>().AddAsync(userLimit);
                user.AddUserLimitId(user, userLimit.Id);
                _unitOfWork.GetRepository<IUserRepository>().Update(user);
            }
            else
            {
                userLimitToday.UpdateAmountOfImagesUploaded(userLimitToday, userLimitToday.AmountOfImagesUploaded + 1);
                userLimitToday.UpdateAmountOfMBUploaded(userLimitToday, userLimitToday.AmountOfMBUploaded + notification.ImageSize);
                _unitOfWork.GetRepository<IUserLimitRepository>().Update(userLimitToday);
            }

            _unitOfWork.GetRepository<IPostRepository>().Update(post);
            await _unitOfWork.CommitAsync();

            await _databaseLogger.LogUserAction(UserActions.UserLoggedIn,
                $"User has succesfully created a new post",
                user.Id);
        }
    }
}
