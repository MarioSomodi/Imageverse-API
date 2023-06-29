namespace Imageverse.Contracts.UserLimits
{
    public record UserLimitResponse(
        double AmountOfMBUploaded,
        int AmountOfImagesUploaded,
        bool RequestedChangeOfPackage);
}
