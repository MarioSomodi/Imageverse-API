namespace Imageverse.Contracts.Authentication
{
    public record AuthenticationResponse
    (
        int Id,
        int PackageId,
        string Username,
        string Name,
        string Surname,
        string Email,
        string ProfileImage,
        string Token
    );
}
