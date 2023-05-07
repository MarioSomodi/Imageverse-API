using Imageverse.Application.Users.Commands.Update;
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
            config.NewConfig<UserUpdateRequest, UserUpdateCommand>();

            config.NewConfig<UserStatistics, UserStatisticsResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);
            config.NewConfig<User, UserResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.PackageId, src => src.PackageId.Value)
                .Map(dest => dest.PostIds, src => src.PostIds.Select(pIds => pIds.Value))
                .Map(dest => dest.UserActionLogIds, src => src.UserActionLogIds.Select(uALids => uALids.Value))
                .Map(dest => dest.UserLimitIds, src => src.UserLimitIds.Select(uLIds => uLIds.Value));
        }
    }
}
