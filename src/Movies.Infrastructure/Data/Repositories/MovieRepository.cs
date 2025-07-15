using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Domain.Interfaces;

namespace Movies.Infrastructure.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext db) : base(db)
        { }

        public async Task AddActorToMovieAsync(int movieId, int actorId)
        {
            var movieActor = new MovieActor { MovieId = movieId, ActorId = actorId };
            await _db.MovieActors.AddAsync(movieActor);
        }

        public async Task AddTagToMovieAsync(int movieId, int tagId)
        {
            var movieTag = new MovieTag { MovieId = movieId, TagId = tagId };
            await _db.MovieTags.AddAsync(movieTag);
        }

        public async Task<IEnumerable<Movie>> GetByActorAsync(int actorId)
        {
            return await _db.MovieActors
                .Where(ma => ma.ActorId == actorId)
                .Join(_db.Movies,
                    ma => ma.MovieId,
                    m => m.Id,
                    (ma, m) => new Movie
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Rating = m.Rating,
                        Director = m.Director
                    })
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByDirectorAsync(string director)
        {
            return await _db.Movies
                .Where(m => m.Director.Equals(director))
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByTagAsync(int tagId)
        {
            return await _db.MovieTags
                .Where(mt => mt.TagId == tagId)
                .Join(_db.Movies,
                    mt => mt.MovieId,
                    m => m.Id,
                    (mt, m) => new Movie
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Rating = m.Rating,
                        Director = m.Director
                    })
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetHighRatedMoviesAsync(double rating)
        {
            return await _db.Movies
                .Where(m => m.Rating >= rating)
                .OrderByDescending(m => m.Rating)
                .ToListAsync();
        }
    }
}