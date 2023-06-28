using ErrorOr;
using Imageverse.Domain.UserLimitAggregate;
using MediatR;

namespace Imageverse.Application.UserLimits.Queries
{
    public record UserLimitOnDateQuery(
        string Id,
        string Date) : IRequest<ErrorOr<UserLimit>>;
}
