using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.Common.Utils;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;
using Imageverse.Domain.UserAggregate;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.Enums;

namespace Imageverse.Application.Users.Commands.UserProfileImageChange
{
    public class UserProfileImageChangeHandler : IRequestHandler<UserProfileImageChangeCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAWSHelper _aWSHelper;
        private readonly IDatabaseLogger _databaseLogger;

        public UserProfileImageChangeHandler(IUnitOfWork unitOfWork, IAWSHelper aWSHelper, IDatabaseLogger databaseLogger)
        {
            _unitOfWork = unitOfWork;
            _aWSHelper = aWSHelper;
            _databaseLogger = databaseLogger;
        }

        public async Task<ErrorOr<bool>> Handle(UserProfileImageChangeCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(UserId.Create(id)) is not User userToUpdate)
            {
                return Errors.Common.NotFound(nameof(User));
            }
            if(ByteConversions.ConvertBytesToMegabytes(request.Image.Length) > 25)
            {
                return Errors.Common.BadRequest("Max profile image size allowed is 25MB");
            }
            if (!FileHelper.CheckIfFileIsImage(FileHelper.GetBytes(request.Image)))
            {
                return Errors.Common.BadRequest("Unsuported image format. Suported image formats are jpeg, jpeg 2000, png and bmp");
            }

            string fileExtension = Path.GetExtension(request.Image.FileName);
            string imageLocation = $"profileImages/{id}{fileExtension}";

            await _aWSHelper.UploadFileAsync(request.Image, imageLocation);
            string imageUrl = _aWSHelper.GetPresignedUrlForResource(imageLocation);

            userToUpdate.UpdateProfileImage(userToUpdate, imageUrl);
            _unitOfWork.GetRepository<IUserRepository>().Update(userToUpdate);

            await _databaseLogger.LogUserAction(UserActions.UserChangedProfileImage, "User has successfully updated their profile image.", userToUpdate.Id);
            
            return await _unitOfWork.CommitAsync();
        }
    }
}
