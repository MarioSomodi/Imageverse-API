using ErrorOr;

namespace Imageverse.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        ErrorOr<AuthenticationResult> Login(string email,
            string password);
        ErrorOr<AuthenticationResult> Register(int packageId,
            string username,
            string name,
            string surname,
            string email,
            string profileImage,
            string password);
    }
}
