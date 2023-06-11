using ErrorOr;
using Imageverse.Application.UserLimits.Queries;
using Imageverse.Contracts.UserLimits;
using Imageverse.Domain.UserLimitAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    public class UserLimitController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public UserLimitController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{date}/{id}")]
        public async Task<IActionResult> Get(string date, string id)
        {
            UserLimitOnDateQuery userLimitOnDateQuery = new UserLimitOnDateQuery(id, date);

            ErrorOr<UserLimit> result = await _mediator.Send(userLimitOnDateQuery);

            return result.Match(
                result => Ok(_mapper.Map<UserLimitResponse>(result)),
                errors => Problem(errors));
        }
    }
}
