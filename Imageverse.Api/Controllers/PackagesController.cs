using ErrorOr;
using Imageverse.Application.Packages.Queries.GetById;
using Imageverse.Contracts.Packages;
using Imageverse.Domain.PackageAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    public class PackagesController : ApiController
    {

        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PackagesController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            GetPackageByIdQuery getByIdQuery = new GetPackageByIdQuery(id);

            ErrorOr<Package> result = await _mediator.Send(getByIdQuery);

            return result.Match(
                result => Ok(_mapper.Map<PackageResponse>(result)),
                errors => Problem(errors));
        }
    }
}
