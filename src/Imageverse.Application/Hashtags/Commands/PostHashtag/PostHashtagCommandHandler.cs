using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.HashtagAggregate;
using MediatR;

namespace Imageverse.Application.Hashtags.Commands.PostHashtag
{
    public class PostHashtagCommandHandler : IRequestHandler<PostHashtagCommand, ErrorOr<IEnumerable<Hashtag>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashtagRepository _hashtagRepository;

        public PostHashtagCommandHandler(IUnitOfWork unitOfWork, IHashtagRepository hashtagRepository)
        {
            _unitOfWork = unitOfWork;
            _hashtagRepository = hashtagRepository;
        }

        public async Task<ErrorOr<IEnumerable<Hashtag>>> Handle(PostHashtagCommand request, CancellationToken cancellationToken)
        {
            List<Hashtag> hashtags = new();
            foreach (string name in request.Names)
            {
                hashtags.Add(Hashtag.Create(name));
            }
            await _hashtagRepository.AddRangeAsync(hashtags);
            await _unitOfWork.CommitAsync();
            return hashtags;
        }
    }
}
