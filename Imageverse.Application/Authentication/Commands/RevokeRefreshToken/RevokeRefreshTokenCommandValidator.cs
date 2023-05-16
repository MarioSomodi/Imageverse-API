using FluentValidation;

namespace Imageverse.Application.Authentication.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
    {
        public RevokeRefreshTokenCommandValidator()
        {
            RuleFor(rRTC => rRTC.Id)
                .NotEmpty();
        }
    }
}
