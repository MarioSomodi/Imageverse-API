using Imageverse.Application.Authentication.Commands.Register;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Authentication.Queries.Login;
using Imageverse.Contracts.Authentication;
using Imageverse.Contracts.UserStatistics;
using Imageverse.Domain.UserAggregate.Entities;
using Mapster;

namespace Imageverse.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Configs without custom mappings are redundant but they are here for future proofing
            //and also to see all used mappings
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<UserStatistics, UserStatisticsResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Id, src => src.User.Id.Value)
                .Map(dest => dest.PackageId, src => src.User.PackageId.Value)
                .Map(dest => dest.PostIds, src => src.User.PostIds.Select(pIds => pIds.Value))
                .Map(dest => dest.UserActionLogIds, src => src.User.UserActionLogIds.Select(uALids => uALids.Value))
                .Map(dest => dest.UserLimitIds, src => src.User.UserLimitIds.Select(uLIds => uLIds.Value))
                .Map(dest => dest, src => src.User);
        }
    }
}
