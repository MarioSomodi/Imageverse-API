namespace Imageverse.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
        T GetRepository<T>() where T : class;
    }
}
