using ErrorOr;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.Errors;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPublisher _mediator;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher, IPublisher mediator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mediator = mediator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetSingleOrDefaultByPropertyValueAsync(nameof(query.Email), query.Email) is not User user
                || !_passwordHasher.VerifyPassword(query.Password, user.Password, user.Salt))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            await _mediator.Publish(new UserLoggedIn(user));

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
