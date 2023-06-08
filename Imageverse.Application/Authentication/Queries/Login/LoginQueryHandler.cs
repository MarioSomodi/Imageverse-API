using ErrorOr;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPublisher _mediator;
        private readonly IAWSHelper _aWSHelper;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher, IPublisher mediator, IUnitOfWork unitOfWork, IAWSHelper aWSHelper)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _aWSHelper = aWSHelper;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.GetRepository<IUserRepository>().GetSingleOrDefaultAsync(u => ((query.AuthenticationType == (int)AuthenticationType.Default) && u.Email == query.Email) || 
                                                                                                ((query.AuthenticationType != (int)AuthenticationType.Default) && u.AuthenticationProviderId == query.AuthenticationProviderId)) 
                                                                                                is not User user
                || !_passwordHasher.VerifyPassword(query.Password, user.Password, user.Salt) && (query.AuthenticationType == (int)AuthenticationType.Default))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if(DateOnly.FromDateTime(user.RefreshTokenExpiry) < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                RefreshTokenResult refreshTokenResult = _jwtTokenGenerator.GenerateRefreshToken();
                user.UpdateRefreshToken(user, refreshTokenResult.RefreshToken);
                user.UpdateRefreshTokenExpiry(user, refreshTokenResult.RefreshTokenExpiry);
                _unitOfWork.GetRepository<IUserRepository>().Update(user);
                await _unitOfWork.CommitAsync();
            }
            

            string profileImageUrl = _aWSHelper.RegeneratePresignedUrlForResourceIfUrlExpired(user.ProfileImage, $"profileImages/{user.Id}", out bool expired);

            //ProfileImageUrl will be an empty string when the profile image is not a url to an s3 resource so no url regeneration is needed
            if (expired && profileImageUrl != string.Empty)
            {
                user.UpdateProfileImage(user, profileImageUrl);
                _unitOfWork.GetRepository<IUserRepository>().Update(user);
                await _unitOfWork.CommitAsync();
            }
            
            var token = _jwtTokenGenerator.GenerateToken(user);

            await _mediator.Publish(new UserLoggedIn(user));

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
