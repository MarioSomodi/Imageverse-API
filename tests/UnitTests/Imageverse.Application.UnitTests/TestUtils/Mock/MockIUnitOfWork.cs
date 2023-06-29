using Imageverse.Application.Common.Interfaces;
using Moq;

namespace Imageverse.Application.UnitTests.TestUtils.Mock
{
	public static class MockIUnitOfWork
	{
		public static Mock<IUnitOfWork> getMockIUnitOfWork()
		{
			Mock<IUnitOfWork> mockUnitOfWork = new();
			mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(() => true);
			return mockUnitOfWork;
		}
	}
}
