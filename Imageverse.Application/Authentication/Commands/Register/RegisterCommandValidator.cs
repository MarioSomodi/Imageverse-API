using FluentValidation;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.Common.Utils;

namespace Imageverse.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(rC => rC.AuthenticationType).NotEmpty();
            RuleFor(rC => rC.Username)
                .NotEmpty();
            RuleFor(rC => rC.Name)
                .NotEmpty();
            RuleFor(rC => rC.Surname)
                .NotEmpty();
            RuleFor(rC => rC.PackageId)
                .NotEmpty()
                .WithName("Package");
            RuleFor(rC => rC.Email)
                .NotEmpty()
                .EmailAddress();
            When(rC => rC.AuthenticationType == ((int)AuthenticationType.Default), () =>
            {
                RuleFor(rC => rC.Password)
                    .NotEmpty()
                    .MinimumLength(6)
                        .WithMessage("The lenght of password must be at least 6 characters. You entered {TotalLength} characters.")
                    .Matches(RegexExpressions.lowercase)
                        .WithMessage("Password must contain at least one lowercase letter.")
                    .Matches(RegexExpressions.uppercase)
                        .WithMessage("Password must contain at least one uppercase letter.")
                    .Matches(RegexExpressions.symbol)
                        .WithMessage("Password must contain at least one special symbol.")
                    .Matches(RegexExpressions.digit)
                        .WithMessage("Password must contain at least one number.");
            });
            When(rC => (rC.AuthenticationType == ((int)AuthenticationType.Google)) || (rC.AuthenticationType == ((int)AuthenticationType.Github)), () =>
            {
                RuleFor(rC => rC.AuthenticationProviderId).NotEmpty();
            });
        }
    }
}
