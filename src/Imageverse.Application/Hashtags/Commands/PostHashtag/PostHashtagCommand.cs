﻿using ErrorOr;
using Imageverse.Domain.HashtagAggregate;
using MediatR;

namespace Imageverse.Application.Hashtags.Commands.PostHashtag
{
    public record PostHashtagCommand(
        IEnumerable<string> Names) : IRequest<ErrorOr<IEnumerable<Hashtag>>>;
}
