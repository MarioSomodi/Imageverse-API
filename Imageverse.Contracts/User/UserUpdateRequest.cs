namespace Imageverse.Contracts.User
{
    public record UserUpdateRequest(
        string Id,
        string Username,
        string Name,
        string Surname);
}
