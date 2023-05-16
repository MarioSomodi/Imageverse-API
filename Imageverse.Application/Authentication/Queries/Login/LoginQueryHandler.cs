using ErrorOr;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common;
using Imageverse.Domain.Common.AppErrors;
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

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher, IPublisher mediator, IUnitOfWork unitOfWork)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.GetRepository<IUserRepository>().GetSingleOrDefaultAsync(u => u.Email == query.Email) is not User user
                || !_passwordHasher.VerifyPassword(query.Password, user.Password, user.Salt))
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
            
            var token = _jwtTokenGenerator.GenerateToken(user);

            await _mediator.Publish(new UserLoggedIn(user));

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
