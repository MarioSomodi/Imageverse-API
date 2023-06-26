using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Application.Posts.Common;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.HashtagAggregate.ValueObjects;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate;
using MediatR;

namespace Imageverse.Application.Posts.Commands.EditPost
{
	public class EditPostCommandHandler : IRequestHandler<EditPostCommand, ErrorOr<PostResult>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAWSHelper _aWSHelper;
		private readonly IDatabaseLogger _databaseLogger;

		public EditPostCommandHandler(IUnitOfWork unitOfWork, IAWSHelper aWSHelper, IDatabaseLogger databaseLogger)
		{
			_unitOfWork = unitOfWork;
			_aWSHelper = aWSHelper;
			_databaseLogger = databaseLogger;
		}

		public async Task<ErrorOr<PostResult>> Handle(EditPostCommand request, CancellationToken cancellationToken)
		{
			if (!Guid.TryParse(request.Id, out var id))
			{
				return Errors.Common.BadRequest("Invalid Id format.");
			}
			if (await _unitOfWork.GetRepository<IPostRepository>().FindByIdAsync(PostId.Create(id)) is not Post post)
			{
				return Errors.Common.NotFound(nameof(Post));
			}

			User user = (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(post.UserId))!;

			if (!post.Description.Equals(request.Description)) post.UpdateDescription(post, request.Description);
			var hashtags = await _unitOfWork.GetRepository<IHashtagRepository>().FindAllById(post.HashtagIds);
			var updatedHashtagIds = new List<HashtagId>();
			
			foreach(var hashtag in request.Hashtags)
			{
				if(await _unitOfWork.GetRepository<IHashtagRepository>().GetFirstOrDefaultAsync(h => h.Name == hashtag) is Hashtag existedHashtag)
				{
					updatedHashtagIds.Add(existedHashtag.Id);
				}
				else
				{
					Hashtag hashtagToAdd = Hashtag.Create(hashtag);
					await _unitOfWork.GetRepository<IHashtagRepository>().AddAsync(hashtagToAdd);
					updatedHashtagIds.Add(hashtagToAdd.Id);
				}
			}
			post.UpdateHashtagIds(post, updatedHashtagIds);
			

			post.UpdateUpdatedAtDateTime(post, DateTime.UtcNow);

			user.UserStatistics.UpdateTotalTimesPostsWereEdited(user.UserStatistics, user.UserStatistics.TotalTimesPostsWereEdited + 1);

			_unitOfWork.GetRepository<IPostRepository>().Update(post);
			_unitOfWork.GetRepository<IUserRepository>().Update(user);

			string postImage = _aWSHelper.RegeneratePresignedUrlForResourceIfUrlExpired(post.Images.First().Url, $"posts/{user?.Id.Value}/{post.Images.First().Name}", out bool expired);

			if (expired && postImage != string.Empty)
			{
				post.UpdateImageUrl(post, postImage);
				_unitOfWork.GetRepository<IPostRepository>().Update(post);
			}

			string profileImageUrl = _aWSHelper.RegeneratePresignedUrlForResourceIfUrlExpired(user!.ProfileImage, $"profileImages/{user.Id.Value}", out bool expiredPP);

			//ProfileImageUrl will be an empty string when the profile image is not a url to an s3 resource so no url regeneration is needed
			if (expiredPP && profileImageUrl != string.Empty)
			{
				user.UpdateProfileImage(user, profileImageUrl);
				_unitOfWork.GetRepository<IUserRepository>().Update(user);
			}

			await _databaseLogger.LogUserAction(UserActions.UserLoggedIn,
				$"User has succesfully edited a post",
				user.Id);

			await _unitOfWork.CommitAsync();

			PostResult result = new PostResult(
				post,
				$"{user.Name} {user.Surname}",
				user.ProfileImage,
				await _unitOfWork.GetRepository<IHashtagRepository>().FindAllById(updatedHashtagIds)
			);
			return result;
		}
	}
}
