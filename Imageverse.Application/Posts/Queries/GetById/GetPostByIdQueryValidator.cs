using FluentValidation;

namespace Imageverse.Application.Posts.Queries.GetById
{
	public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
	{
		public GetPostByIdQueryValidator()
		{
			RuleFor(gPPPQ => gPPPQ.Id).NotEmpty();
		}
	}
}
