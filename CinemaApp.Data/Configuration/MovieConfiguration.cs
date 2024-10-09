namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static Common.EntityValidationConstants.Movie;
    using static Common.ApplicationConstants;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Fluent API
            builder.HasKey(m => m.Id);

            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder
                .Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(GenreMaxLength);

            builder
                .Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(DirectorNameMaxLength);

            builder
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder
                .Property(m => m.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength)
                .HasDefaultValue(NoImageUrl);

            builder.HasData(this.SeedMovies());
        }

        private List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "Harry Potter and the Goblet of Fire",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2005, 11, 01),
                    Director = "Mike Newel",
                    Duration = 157,
                    Description = "Harry Potter and the Goblet of Fire is a 2005 fantasy film directed by Mike Newell from a screenplay by Steve Kloves. It is based on the 2000 novel Harry Potter and the Goblet of Fire by J. K. Rowling."
                },
                new Movie()
                {
                    Title = "Lord of the Rings",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2001, 05, 01),
                    Director = "Peter Jackson",
                    Duration = 178,
                    Description = "The Lord of the Rings: The Fellowship of the Ring is a 2001 epic high fantasy adventure film directed by Peter Jackson from a screenplay by Fran Walsh, Philippa Boyens, and Jackson, based on 1954's The Fellowship of the Ring, the first volume of the novel The Lord of the Rings by J. R. R. Tolkien. "
                }
            };

            return movies;
        }
    }
}
