namespace Imageverse.Contracts.Authentication
{
    public record RegisterRequest
    (
        int PackageId,
        string Name,
        string Username,
        string Surname,
        string Email,
        string ProfileImage,
        string Password
    );
}
