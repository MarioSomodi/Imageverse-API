using ErrorOr;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.Common.Errors;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Entities;
using MediatR;

namespace Imageverse.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            //TODO add Missing error check in the create function of aggregates
            //TODO -> will have asyncronus logic this is just for now to stop the warning because it is bugging me
            await Task.CompletedTask;
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }
            //Implement controller, repository and mediator logic for creating the package -> this is a mock for now.
            PackageId packageId = PackageId.CreateUnique();

            //Implement the logic for creating the initial userStatistic object -> this is a mock for now.
            UserStatistics userStatistics = UserStatistics.Create(1,1,1,1,1);

            User user = User.Create(
                command.Username,
                command.Name,
                command.Surname,
                command.Email,
                command.ProfileImage,
                command.Password,
                packageId,
                userStatistics);

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
