using Movies.Domain.Interfaces;
using Movies.Infrastructure.Data.Repositories;

namespace Movies.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Movies = new MovieRepository(db);
        }

        public IMovieRepository Movies { get; }

        public async Task<int> CommitAsync() => await _db.SaveChangesAsync();

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}