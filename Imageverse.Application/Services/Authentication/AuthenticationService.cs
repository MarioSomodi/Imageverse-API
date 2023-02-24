using ErrorOr;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.Errors;
using Imageverse.Domain.Entities;

namespace Imageverse.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user
                || user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public ErrorOr<AuthenticationResult> Register(int packageId, string username, string name, string surname, string email, string profileImage, string password)
        {
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            User user = new()
            {
                Email = email,
                Name = name,
                PackageId = packageId,
                Password = password,
                ProfileImage = profileImage,
                Surname = surname,
                Username = username,
            };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
