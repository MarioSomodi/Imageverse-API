using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate;
using MediatR;

namespace Imageverse.Application.Authentication.Queries.UserExists
{
    public class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(UserExistsQuery query, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.GetRepository<IUserRepository>().GetSingleOrDefaultAsync(u => u.AuthenticationProviderId == query.AuthenticationProviderId) is not User user)
            {
                return false;
            }
            return true;
        }
    }
}
