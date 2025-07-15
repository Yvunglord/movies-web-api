using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Human> Humans { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<MovieActor> MovieActors { get; set; }
        public virtual DbSet<MovieTag> MovieTags { get; set; }
        public virtual DbSet<Movie> SimilarMovies { get; set; }

        public AppDbContext()
        { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Title)
                .IsRequired();
            });

            modelBuilder.Entity<Human>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Name)
                .IsRequired();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name)
                .IsRequired();
            });

            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasKey(ma => new { ma.MovieId, ma.ActorId });
            });

            modelBuilder.Entity<MovieActor>()
                .HasOne<Human>()
                .WithMany()
                .HasForeignKey(ma => ma.ActorId);

            modelBuilder.Entity<MovieActor>()
                .HasOne<Movie>()
                .WithMany()
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieTag>(entity =>
            {
                entity.HasKey(mt => new { mt.MovieId, mt.TagId });
            });

            modelBuilder.Entity<MovieTag>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(mt => mt.TagId);

            modelBuilder.Entity<MovieTag>()
                .HasOne<Movie>()
                .WithMany()
                .HasForeignKey(mt => mt.MovieId);

            modelBuilder.Entity<SimilarMovie>(entity =>
            {
                entity.HasKey(sm => new { sm.MovieId, sm.SimilarMovieId });
                entity.Property(sm => sm.Score)
                .IsRequired();
            });
        }
    }
}