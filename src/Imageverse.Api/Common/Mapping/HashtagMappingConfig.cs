using Imageverse.Application.Hashtags.Commands.PostHashtag;
using Imageverse.Contracts.Hashtag;
using Imageverse.Domain.HashtagAggregate;
using Mapster;

namespace Imageverse.Api.Common.Mapping
{
    public class HashtagMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PostHashtagRequest, PostHashtagCommand>();

            config.NewConfig<Hashtag, HashtagResponse>()
                  .Map(dest => dest.Id, src => src.Id.Value);
        }
    }
}
