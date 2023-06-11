using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.HashtagAggregate;
using MediatR;

namespace Imageverse.Application.Hashtags.Commands
{
    public class PostHashtagCommandHandler : IRequestHandler<PostHashtagCommand, ErrorOr<IEnumerable<Hashtag>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostHashtagCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<IEnumerable<Hashtag>>> Handle(PostHashtagCommand request, CancellationToken cancellationToken)
        {
            List<Hashtag> hashtags = new(); 
            foreach(string name in request.Names)
            {
                hashtags.Add(Hashtag.Create(name));
            }
            await _unitOfWork.GetRepository<IHashtagRepository>().AddRangeAsync(hashtags);
            await _unitOfWork.CommitAsync();
            return hashtags;
        }
    }
}
