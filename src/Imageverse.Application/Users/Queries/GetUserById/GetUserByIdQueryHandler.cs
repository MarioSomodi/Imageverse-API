using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAWSHelper _aWSHelper;

		public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IAWSHelper aWSHelper)
		{
			_unitOfWork = unitOfWork;
			_aWSHelper = aWSHelper;
		}

		public async Task<ErrorOr<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(UserId.Create(id)) is not User user)
            {
                return Errors.Common.NotFound(nameof(User));
			}

			string profileImageUrl = _aWSHelper.RegeneratePresignedUrlForResourceIfUrlExpired(user.ProfileImage, $"profileImages/{user.Id.Value}", out bool expired);

			//ProfileImageUrl will be an empty string when the profile image is not a url to an s3 resource so no url regeneration is needed
			if (expired && profileImageUrl != string.Empty)
			{
				user.UpdateProfileImage(user, profileImageUrl);
				_unitOfWork.GetRepository<IUserRepository>().Update(user);
				await _unitOfWork.CommitAsync();
			}

			return user;
        }
    }
}
