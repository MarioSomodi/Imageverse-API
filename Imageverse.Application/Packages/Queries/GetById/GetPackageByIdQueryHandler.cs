﻿using ErrorOr;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.Errors;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Packages.Queries.GetById
{
    public class GetPackageByIdQueryHandler : IRequestHandler<GetPackageByIdQuery, ErrorOr<Package>>
    {
        private readonly IPackageRepository _packageRepository;

        public GetPackageByIdQueryHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<ErrorOr<Package>> Handle(GetPackageByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _packageRepository.GetByIdAsync(PackageId.Create(new Guid(request.Id))) is not Package package)
            {
                return Errors.Common.NotFound(nameof(Package));
            }
            return package;
        }
    }
}
