namespace Imageverse.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int packageId, string username, string name, string surname, string email, string profilePicture, int id);
    }
}
