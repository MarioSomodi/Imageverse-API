using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Application.Posts.Common;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.Common.Utils;
using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.Entites;
using Imageverse.Domain.PostAggregate.Events;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Imageverse.Domain.UserLimitAggregate;
using MediatR;
using System.Drawing.Imaging;

namespace Imageverse.Application.Posts.Commands.CreatePost
{
	public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<PostResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;
		private readonly IPackageRepository _packageRepository;
		private readonly IUserLimitRepository _userLimitRepository;
		private readonly IPostRepository _postRepository;
		private readonly IHashtagRepository _hashtagRepository;
		private readonly IAWSHelper _aWSHelper;
        private readonly IPublisher _publisher;

		public CreatePostCommandHandler(IUnitOfWork unitOfWork, IAWSHelper aWSHelper, IPublisher publisher, IUserRepository userRepository, IPackageRepository packageRepository, IUserLimitRepository userLimitRepository, IPostRepository postRepository, IHashtagRepository hashtagRepository)
		{
			_unitOfWork = unitOfWork;
			_aWSHelper = aWSHelper;
			_publisher = publisher;
			_userRepository = userRepository;
			_packageRepository = packageRepository;
			_userLimitRepository = userLimitRepository;
			_postRepository = postRepository;
			_hashtagRepository = hashtagRepository;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public async Task<ErrorOr<PostResult>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            User user = (await _userRepository.FindByIdAsync(UserId.Create(Guid.Parse(request.UserId))))!;
            Package package = (await _packageRepository.FindByIdAsync(user.PackageId))!;
            UserLimit? userLimitToday = _userLimitRepository.GetUserLimitIfExistsForDate(DateOnly.FromDateTime(DateTime.UtcNow), user.UserLimitIds.ToList());

            byte[] imageBytes = Convert.FromBase64String(request.Base64Image);
            double imageSizeInMB = 0;
            string imageResolution = "";
            string imageName = "";
            string key = "";
            string imageUrl = "";

            using (Stream stream = new MemoryStream(imageBytes))
            {
                imageSizeInMB = ByteConversions.ConvertBytesToMegabytes(stream.Length);
                if (imageSizeInMB > Convert.ToDouble(package.UploadSizeLimit))
                {
                    return Errors.Common.MethodNotAllowed($"Highest image size you can upload is {package.UploadSizeLimit} and you are trying to upload {imageSizeInMB}");
                }
                if (userLimitToday is not null && userLimitToday.AmountOfImagesUploaded + 1 > package.DailyImageUploadLimit)
                {
                    return Errors.Common.MethodNotAllowed($"You are not allowed to post anymore today by the restrictions in your package");
                }
                if (userLimitToday is not null && userLimitToday.AmountOfMBUploaded + imageSizeInMB > package.DailyUploadLimit)
                {
                    return Errors.Common.MethodNotAllowed($"You are not allowed to post anymore today by restrictions in your package");
                }
                byte[]? imageToUploadToAWS = null;
                using (var outStream = new MemoryStream())
                {
                    var imageStream = System.Drawing.Image.FromStream(stream);
                    ImageFormat format = ImageFormat.Jpeg;
                    if (request.SaveImageAs == "png")
                    {
                        format = ImageFormat.Png;
                    }
                    else if (request.SaveImageAs == "bmp")
                    {
                        format = ImageFormat.Bmp;
                    }
                    imageStream.Save(outStream, format);
                    imageResolution = $"{imageStream.Width}x{imageStream.Height}";
                    imageToUploadToAWS = outStream.ToArray();
                }
                imageName = Guid.NewGuid().ToString();

                key = $"posts/{user.Id.Value}/{imageName}.{request.SaveImageAs}";

                await _aWSHelper.UploadByteArrayAsync(imageToUploadToAWS, key);
                imageUrl = _aWSHelper.GetPresignedUrlForResource(key);
            }

            List<Hashtag> hashtags = new List<Hashtag>();

            foreach (var hashtag in request.Hashtags)
            {
                if (await _hashtagRepository.GetSingleOrDefaultAsync(h => h.Name.Equals(hashtag)) is not Hashtag hashtagThatExists)
                {
                    Hashtag hashtagToAdd = Hashtag.Create(hashtag);
                    await _hashtagRepository.AddAsync(hashtagToAdd);
                    hashtags.Add(hashtagToAdd);
                }
                else
                {
                    hashtags.Add(hashtagThatExists);
                }
            }

            Image image = Image.Create(
                        imageName,
                        imageUrl,
                        imageSizeInMB,
                        imageResolution,
                        request.SaveImageAs);

            Post post = Post.Create(
                    request.Description,
                    user.Id,
                    new List<Image>() {
                        image
                    }
                );

            PostResult result = new PostResult(
                post,
                $"{user.Name} {user.Surname}",
                user.ProfileImage,
                hashtags
            );

            await _postRepository.AddAsync(post);

            bool success = await _unitOfWork.CommitAsync();
            if (success) await _publisher.Publish(new PostCreated(user, hashtags, post, imageSizeInMB));
            return result;
        }
    }
}
