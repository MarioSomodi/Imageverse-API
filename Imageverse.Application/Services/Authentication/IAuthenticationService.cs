namespace Imageverse.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string email,
            string password);
        AuthenticationResult Register(int packageId,
            string username,
            string name,
            string surname,
            string email,
            string profilePicture,
            string password);
    }
}
