using Imageverse.Application.Packages.Commands.CreatePackage;
using Imageverse.Contracts.Packages;
using Imageverse.Contracts.UserLimits;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.UserLimitAggregate;
using Mapster;

namespace Imageverse.Api.Common.Mapping
{
	public class UserLimitMappingConfig
	{
		public void Register(TypeAdapterConfig config)
		{
			//Configs without custom mappings are redundant but they are here for future proofing
			//and also to see all used mappings
			config.NewConfig<UserLimit, UserLimitResponse>()
				.Map(dest => dest.AmountOfMBUploaded, source => Math.Round(source.AmountOfMBUploaded, 1));
		}
	}
}
