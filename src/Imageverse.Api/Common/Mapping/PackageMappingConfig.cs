using Imageverse.Application.Packages.Commands.CreatePackage;
using Imageverse.Contracts.Packages;
using Imageverse.Domain.PackageAggregate;
using Mapster;

namespace Imageverse.Api.Common.Mapping
{
    public class PackageMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Configs without custom mappings are redundant but they are here for future proofing
            //and also to see all used mappings
            config.NewConfig<CreatePackageRequest, CreatePackageCommand>();

            config.NewConfig<Package, PackageResponse>()
                  .Map(dest => dest.Id, src => src.Id.Value);
        }
    }
}
