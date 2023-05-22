using ErrorOr;
using Imageverse.Application.Packages.Commands.CreatePackage;
using Imageverse.Application.Packages.Queries.GetAllPackages;
using Imageverse.Application.Packages.Queries.GetPackageById;
using Imageverse.Contracts.Packages;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Infrastructure.Authentication;
using Mapster;
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

        [Admin]
        [HttpPost]
        public async Task<IActionResult> CreatePackage(CreatePackageRequest createPackageRequest)
        {
            CreatePackageCommand createPackageCommand = _mapper.Map<CreatePackageCommand>(createPackageRequest);

            ErrorOr<Package> result = await _mediator.Send(createPackageCommand);

            return result.Match(
                result => Ok(_mapper.Map<PackageResponse>(result)),
                errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ErrorOr<IEnumerable<Package>> result = await _mediator.Send(new GetAllPackagesQuery());

            return result.Match(
                result => Ok(result.AsQueryable().ProjectToType<PackageResponse>()),
                errors => Problem(errors));
        }
    }
}
