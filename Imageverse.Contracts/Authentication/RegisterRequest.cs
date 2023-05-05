namespace Imageverse.Contracts.Authentication
{
    public record RegisterRequest(
        string Username,
        string Name,
        string Surname,
        string Email,
        string ProfileImage,
        string Password,
        string PackageId);
}
