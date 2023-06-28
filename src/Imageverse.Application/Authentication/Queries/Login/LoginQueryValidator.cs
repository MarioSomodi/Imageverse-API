using FluentValidation;
using Imageverse.Domain.Common.Enums;

namespace Imageverse.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator() {
            RuleFor(lQ => lQ.AuthenticationType).NotNull();
            When(lQ => lQ.AuthenticationType == ((int)AuthenticationType.Default), () =>
            {
                RuleFor(lQ => lQ.Email).NotEmpty().EmailAddress();
                RuleFor(lQ => lQ.Password).NotEmpty();
            });
            When(lQ => (lQ.AuthenticationType == ((int)AuthenticationType.Google)) || (lQ.AuthenticationType == ((int)AuthenticationType.Github)), () =>
            {
                RuleFor(lQ => lQ.AuthenticationProviderId).NotEmpty();
            });
        }
    }
}
