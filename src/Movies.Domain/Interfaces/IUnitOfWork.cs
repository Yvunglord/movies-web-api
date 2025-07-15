namespace Movies.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository Movies { get; }
        Task<int> CommitAsync();
    }
}