using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Application.Posts.Common;
using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.UserAggregate;
using MediatR;

namespace Imageverse.Application.Posts.Queries.GetPostsPerPage
{
    public class GetPostsPerPageQueryHandler : IRequestHandler<GetPostsPerPageQuery, ErrorOr<IEnumerable<PostResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAWSHelper _awshelper;

        public GetPostsPerPageQueryHandler(IUnitOfWork unitOfWork, IAWSHelper awshelper)
        {
            _unitOfWork = unitOfWork;
            _awshelper = awshelper;
        }

        public async Task<ErrorOr<IEnumerable<PostResult>>> Handle(GetPostsPerPageQuery request, CancellationToken cancellationToken)
        {
            const int postsPerPage = 10;
            int postsToSkip = request.Page == 1 ? 0 : postsPerPage * request.Page - 1;
            List<Post> posts = await Task.FromResult(_unitOfWork.GetRepository<IPostRepository>().SkipAndTakeSpecific(postsToSkip, postsPerPage, p => p.OrderByDescending(pO => pO.UpdatedAtDateTime)).ToList());
            List<PostResult> result = new List<PostResult>();
            foreach (var post in posts)
            {
                List<Hashtag> hashtags = (await _unitOfWork.GetRepository<IHashtagRepository>().FindAllById(post.HashtagIds)).ToList();
                User? user = await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(post.UserId);

                string postImage = _awshelper.RegeneratePresignedUrlForResourceIfUrlExpired(post.Images.First().Url, $"posts/{user?.Id.Value}/{post.Images.First().Name}", out bool expired);

                if (expired && postImage != string.Empty)
                {
                    post.UpdateImageUrl(post, postImage);
                    _unitOfWork.GetRepository<IPostRepository>().Update(post);
                    await _unitOfWork.CommitAsync();
                }

                string profileImageUrl = _awshelper.RegeneratePresignedUrlForResourceIfUrlExpired(user!.ProfileImage, $"profileImages/{user.Id.Value}", out bool expiredPP);

                //ProfileImageUrl will be an empty string when the profile image is not a url to an s3 resource so no url regeneration is needed
                if (expiredPP && profileImageUrl != string.Empty)
                {
                    user.UpdateProfileImage(user, profileImageUrl);
                    _unitOfWork.GetRepository<IUserRepository>().Update(user);
                    await _unitOfWork.CommitAsync();
                }

                result.Add(new PostResult(
                        post,
                        $"{user?.Name} " + $"{user?.Surname}",
                        user!.ProfileImage,
                        hashtags
                    )
                );
            }
            return result;
        }
    }
}
