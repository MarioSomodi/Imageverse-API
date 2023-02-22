namespace Imageverse.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
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
            return new AuthenticationResult
            {
                Username = username,
                PackageId = packageId,
                ProfileImage = profilePicture,
                Surname = surname,
                Name = name,
                Email = email,
                Token = "token",
                Id = 1
            };
        }
    }
}
