namespace CinemaApp.Data
{
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;

    using Models;

    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext()
        {
            
        }

        public CinemaDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;

        public virtual DbSet<Cinema> Cinemas { get; set; } = null!;

        public virtual DbSet<CinemaMovie> CinemasMovies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
