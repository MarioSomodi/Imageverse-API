using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Entities;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Moq;

namespace Imageverse.Application.UnitTests.TestUtils.Mock
{
	public static partial class Repositories
	{
		public static class UserRepository
		{
			public static Mock<IUserRepository> getMockUserRepository()
			{
				Mock<IUserRepository> mockUserRepository = new();
				mockUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<UserId>()))
					.ReturnsAsync((UserId userId) => {
						 var usr = User.Create(
							 "username",
							 "name",
							 "surname",
							 "email",
							 "password",
							 PackageId.CreateUnique(),
							 UserStatistics.Create(1, 1, 1, 0, 1),
							 new byte[0],
							 "refreshToken",
							 "profileImage",
							 DateTime.UtcNow,
							 "0",
							 1);
						 return usr.UpdateId(usr, userId);
					}
				 );
				return mockUserRepository;
			}
		}
	}
}
