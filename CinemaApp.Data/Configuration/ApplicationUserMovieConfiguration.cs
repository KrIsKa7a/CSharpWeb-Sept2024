namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ApplicationUserMovieConfiguration : IEntityTypeConfiguration<ApplicationUserMovie>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserMovie> builder)
        {
            builder
                .HasKey(um => new { um.ApplicationUserId, um.MovieId });

            builder
                .HasOne(um => um.Movie)
                .WithMany(m => m.MovieApplicationUsers)
                .HasForeignKey(um => um.MovieId);

            builder
                .HasOne(um => um.ApplicationUser)
                .WithMany(u => u.ApplicationUserMovies)
                .HasForeignKey(um => um.ApplicationUserId);
        }
    }
}
