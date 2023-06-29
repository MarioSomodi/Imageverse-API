namespace Imageverse.Contracts.User
{
    public record UserPasswordUpdateRequest(
        string Id, 
        string CurrentPassword, 
        string NewPassword);
}
