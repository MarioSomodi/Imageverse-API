﻿using Imageverse.Contracts.UserStatistics;

namespace Imageverse.Contracts.User
{
    public record UserResponse(
        string Id,
        string Username,
        string Name,
        string Surname,
        string Email,
        string ProfileImage,
        string PackageId,
        string ActivePackageId,
        int authenticationType,
        bool isAdmin,
        List<string> PostIds,
        List<string> UserActionLogIds,
        List<string> UserLimitIds,
        UserStatisticsResponse UserStatistics,
        DateTime PackageValidFrom,
        string PreviousPackageId,
        string RefreshToken,
        DateTime RefreshTokenExpiry);
}
