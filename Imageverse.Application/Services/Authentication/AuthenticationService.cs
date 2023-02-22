using Imageverse.Application.Common.Interfaces.Authentication;

namespace Imageverse.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult
            {
                Email = email,
                Token = "token",
                Id = 1,
                Name = "name",
                PackageId = 1,
                ProfileImage = "linkToPfp",
                Surname = "surname",
                Username = "username"
            };
        }

        public AuthenticationResult Register(int packageId, string username, string name, string surname, string email, string profilePicture, string password)
        {
            // TODO check if user alredy exists by checking email

            // TODO add user to db and get back the identity auto generated id

            int tempId = 1;

            //Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(packageId, username, name, surname, email, profilePicture, tempId);

            return new AuthenticationResult
            {
                Username = username,
                PackageId = packageId,
                ProfileImage = profilePicture,
                Surname = surname,
                Name = name,
                Email = email,
                Token = token,
                Id = 1
            };
        }
    }
}
