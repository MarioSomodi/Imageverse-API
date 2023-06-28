namespace Imageverse.Contracts.User
{
    public record UserInfoUpdateRequest(
        string Id,
        string Username,
        string Name,
        string Surname);
}
