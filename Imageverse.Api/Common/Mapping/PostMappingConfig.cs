using Imageverse.Application.Posts.Common;
using Imageverse.Contracts.Images;
using Imageverse.Contracts.Post;
using Imageverse.Contracts.User;
using Imageverse.Domain.PostAggregate.Entites;
using Mapster;

namespace Imageverse.Api.Common.Mapping
{
    public class PostMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Image, ImageResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);

            config.NewConfig<PostResult, PostResponse>()
               .Map(dest => dest.Id, src => src.Post.Id.Value)
               .Map(dest => dest.Description, src => src.Post.Description)
               .Map(dest => dest.PostedAtDateTime, src => src.Post.PostedAtDateTime)
               .Map(dest => dest.UpdatedAtDateTime, src => src.Post.UpdatedAtDateTime)
               .Map(dest => dest.Images, src => src.Post.Images);
        }
    }
}
