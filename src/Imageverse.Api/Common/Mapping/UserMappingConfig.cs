﻿using Imageverse.Application.Users.Commands.UserEmailUpdate;
using Imageverse.Application.Users.Commands.UserInfoUpdate;
using Imageverse.Application.Users.Commands.UserIsAdminChange;
using Imageverse.Application.Users.Commands.UserPackageChange;
using Imageverse.Application.Users.Commands.UserPasswordUpdate;
using Imageverse.Contracts.User;
using Imageverse.Contracts.UserStatistics;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Entities;
using Mapster;

namespace Imageverse.Api.Common.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserInfoUpdateRequest, UserInfoUpdateCommand>();
            config.NewConfig<UserEmailUpdateRequest, UserEmailUpdateCommand>();
            config.NewConfig<UserPasswordUpdateRequest, UserPasswordUpdateCommand>();
            config.NewConfig<UserPackageChangeRequest, UserPackageChangeCommand>();
            config.NewConfig<UserIsAdminChangeRequest, UserIsAdminChangeCommand>();

            config.NewConfig<UserStatistics, UserStatisticsResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.TotalMbUploaded, src => Math.Round(src.TotalMBUploaded,1));
			config.NewConfig<User, UserResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.PackageId, src => src.PackageId.Value)
                .Map(dest => dest.ActivePackageId, src => src.GetUsersActivePackage(src).Value)
                .Map(dest => dest.PreviousPackageId, src => src.PreviousPackageId.Value)
                .Map(dest => dest.PostIds, src => src.PostIds.Select(pIds => pIds.Value))
                .Map(dest => dest.UserActionLogIds, src => src.UserActionLogIds.Select(uALids => uALids.Value))
                .Map(dest => dest.UserLimitIds, src => src.UserLimitIds.Select(uLIds => uLIds.Value));
        }
    }
}
