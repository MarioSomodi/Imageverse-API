namespace Imageverse.Contracts.UserLimits
{
    public record UserLimitResponse(
        int AmountOfMBUploaded,
        int AmountOfImagesUploaded,
        bool RequestedChangeOfPackage);
}
