using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.HashtagAggregate;
using MediatR;

namespace Imageverse.Application.Hashtags.Queries
{
    public class GetAllHashtagsQueryHandler : IRequestHandler<GetAllHashtagsQuery, ErrorOr<IEnumerable<Hashtag>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllHashtagsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<IEnumerable<Hashtag>>> Handle(GetAllHashtagsQuery request, CancellationToken cancellationToken)
        {
            var hashtags = await _unitOfWork.GetRepository<IHashtagRepository>().Get();
            return hashtags.ToList();
        }
    }
}
