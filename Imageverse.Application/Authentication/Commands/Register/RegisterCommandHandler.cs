using ErrorOr;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Entities;
using Imageverse.Domain.UserAggregate.Events;
using MediatR;

namespace Imageverse.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IDatabaseLogger _databaseLogger;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher, IDatabaseLogger databaseLogger)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _databaseLogger = databaseLogger;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetSingleOrDefaultByPropertyValueAsync(nameof(command.Email), command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            UserStatistics userStatistics = UserStatistics.Create(0,0,0,1,0);

            var hashedPassword = _passwordHasher.HashPassword(command.Password, out byte[] salt); 

            User user = User.Create(
                command.Username,
                command.Name,
                command.Surname,
                command.Email,
                command.ProfileImage,
                hashedPassword,
                PackageId.Create(new Guid(command.PackageId)),
                userStatistics,
                salt);

            await _userRepository.AddAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);
            
            await _databaseLogger.LogUserAction(UserActions.UserRegistered,
               $"User {user.Name} {user.Surname} with the username {user.Username} and email {user.Email} has registered succesfully.",
               user.Id);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
