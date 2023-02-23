using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
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

        public AuthenticationResult Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user
                || user.Password != password)
            {
                throw new Exception("Incorrect user credentials.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public AuthenticationResult Register(int packageId, string username, string name, string surname, string email, string profileImage, string password)
        {
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email alredy exists.");
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
