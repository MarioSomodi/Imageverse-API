using ErrorOr;
using Imageverse.Application.Hashtags.Commands.PostHashtag;
using Imageverse.Application.Hashtags.Queries;
using Imageverse.Contracts.Hashtag;
using Imageverse.Domain.HashtagAggregate;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    public class HashtagController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public HashtagController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostHashtagRequest postHashtagRequest)
        {
            PostHashtagCommand postHashtagCommand = _mapper.Map<PostHashtagCommand>(postHashtagRequest);

            ErrorOr<IEnumerable<Hashtag>> result = await _mediator.Send(postHashtagCommand);

            return result.Match(
                result => Ok(result.AsQueryable().ProjectToType<HashtagResponse>()),
                errors => Problem(errors));
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllHashtagsQuery getAllHashtagsQuery = new GetAllHashtagsQuery();

            ErrorOr<IEnumerable<Hashtag>> result = await _mediator.Send(getAllHashtagsQuery);

            return result.Match(
                result => Ok(result.AsQueryable().ProjectToType<HashtagResponse>()),
                errors => Problem(errors));
        }
    }
}
