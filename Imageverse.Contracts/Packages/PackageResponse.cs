namespace Imageverse.Contracts.Packages
{
    public record PackageResponse(
        string Id,
        string Name,
        double Price,
        int UploadSizeLimit,
        int DailyUploadLimit,
        int DailyImageUploadLimit);
}
