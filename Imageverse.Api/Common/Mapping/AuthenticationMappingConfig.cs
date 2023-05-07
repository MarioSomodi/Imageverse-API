using Imageverse.Application.Authentication.Commands.Register;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Authentication.Queries.Login;
using Imageverse.Contracts.Authentication;
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
            config.NewConfig<AuthenticationResult, AuthenticationResponse>();
        }
    }
}
