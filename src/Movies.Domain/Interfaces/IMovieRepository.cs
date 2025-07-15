using Movies.Domain.Entities;
using Movies.Domain.Interfaces.Base;

namespace Movies.Domain.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetByDirectorAsync(string director);
        Task<IEnumerable<Movie>> GetHighRatedMoviesAsync(double rating);
        Task<IEnumerable<Movie>> GetByActorAsync(int actorId);
        Task<IEnumerable<Movie>> GetByTagAsync(int tagId);
        Task AddActorToMovieAsync(int movieId, int actorId);
        Task AddTagToMovieAsync(int movieId, int tagId);
    }
}