namespace Imageverse.Contracts.Packages
{
    public record CreatePackageRequest(
        string Name,
        double Price,
        int UploadSizeLimit,
        int DailyUploadLimit,
        int DailyImageUploadLimit);
}
