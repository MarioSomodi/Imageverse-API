using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Imageverse.Application.Common.CustomValidators
{
	public static class UserExistsValidator
	{
		public static async Task<bool> Exists(IUserRepository userRepository, string userId)
		{
			if (await userRepository.FindByIdAsync(UserId.Create(Guid.Parse(userId))) is not User user)
			{
				return false;
			}
			return true;
		}
	}
}
