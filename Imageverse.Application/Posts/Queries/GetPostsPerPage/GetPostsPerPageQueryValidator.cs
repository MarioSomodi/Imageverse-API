using FluentValidation;

namespace Imageverse.Application.Posts.Queries.GetPostsPerPage
{
    public class GetPostsPerPageQueryValidator : AbstractValidator<GetPostsPerPageQuery>
    {
        public GetPostsPerPageQueryValidator()
        {
            RuleFor(gPPPQ => gPPPQ.Page).NotNull().NotEmpty();
        }
    }
}
