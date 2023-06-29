using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Moq;

namespace Imageverse.Application.UnitTests.TestUtils.Mock
{
	public static partial class Repositories
	{
		public static class PackageRepository
		{
			public static Mock<IPackageRepository> getMockPackageRepository()
			{
				Mock<IPackageRepository> mockPackageRepository = new();
				mockPackageRepository.Setup(x => x.FindByIdAsync(It.IsAny<PackageId>()))
				.ReturnsAsync((PackageId packageId) =>
					{
						var package = Package.Create("name", 1, 1, 1, 1);
						return package.UpdateId(package, packageId);
					}
				);
				return mockPackageRepository;
			}
		}
	}
}
