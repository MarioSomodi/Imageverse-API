using ErrorOr;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common;
using Imageverse.Domain.Common.AppErrors;
using Imageverse.Domain.Common.Enums;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Entities;
using MediatR;

namespace Imageverse.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IDatabaseLogger _databaseLogger;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher, IDatabaseLogger databaseLogger, IUnitOfWork unitOfWork)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
            _databaseLogger = databaseLogger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.GetRepository<IUserRepository>().GetSingleOrDefaultAsync(u => u.Email == command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            UserStatistics userStatistics = UserStatistics.Create(0,0,0,1,0);

            var hashedPassword = _passwordHasher.HashPassword(command.Password, out byte[] salt);

            RefreshTokenResult refreshTokenResult = _jwtTokenGenerator.GenerateRefreshToken();

            User user = User.Create(
                command.Username,
                command.Name,
                command.Surname,
                command.Email,
                hashedPassword,
                PackageId.Create(new Guid(command.PackageId)),
                userStatistics,
                salt,
                refreshTokenResult.RefreshToken,
                refreshTokenResult.RefreshTokenExpiry);

            await _unitOfWork.GetRepository<IUserRepository>().AddAsync(user);
            await _unitOfWork.CommitAsync();

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
