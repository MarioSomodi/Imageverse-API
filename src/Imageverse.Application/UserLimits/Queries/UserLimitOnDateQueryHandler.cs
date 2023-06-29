using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Domain.UserLimitAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.UserLimits.Queries
{
    public class UserLimitOnDateQueryHandler : IRequestHandler<UserLimitOnDateQuery, ErrorOr<UserLimit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserLimitOnDateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<UserLimit>> Handle(UserLimitOnDateQuery query, CancellationToken cancellationToken)
        {
            if(!DateOnly.TryParseExact(query.Date, "yyyy-MM-dd", out DateOnly onDate))
            {
                return Errors.Common.BadRequest($"Date expected in format yyyy-MM-dd but got {query.Date}");
            }
            if (!Guid.TryParse(query.Id, out var id))
            {
                return Errors.Common.BadRequest("Invalid Id format.");
            }
            if (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(UserId.Create(id)) is not User user)
            {
                return Errors.Common.NotFound(nameof(User));
            }
            
            if (await Task.Run(() => _unitOfWork.GetRepository<IUserLimitRepository>().GetUserLimitIfExistsForDate(onDate, user.UserLimitIds.ToList())) is not UserLimit userLimitOnDate){
                userLimitOnDate = UserLimit.Create(0, 0, false);
                await _unitOfWork.GetRepository<IUserLimitRepository>().AddAsync(userLimitOnDate);
                await _unitOfWork.CommitAsync();
            }
            return userLimitOnDate;
        }
    }
}
