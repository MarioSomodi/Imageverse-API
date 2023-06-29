namespace Imageverse.Contracts.User
{
    public record UserEmailUpdateRequest(
        string Id,
        string Email,
        int authenticationType);
}
