namespace CinemaApp.Data.Seeding
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    using DataTransferObjects;
    using Models;
    using Services.Mapping;

    public static class DbSeeder
    {
        public static async Task SeedMoviesAsync(IServiceProvider services, string jsonPath)
        {
            await using CinemaDbContext dbContext = services
                .GetRequiredService<CinemaDbContext>();

            ICollection<Movie> allMovies = await dbContext
                .Movies
                .ToArrayAsync();
            try
            {
                string jsonInput = await File
                    .ReadAllTextAsync(jsonPath, Encoding.ASCII, CancellationToken.None);
                
                ImportMovieDto[] movieDtos =
                    JsonConvert.DeserializeObject<ImportMovieDto[]>(jsonInput);
                foreach (ImportMovieDto movieDto in movieDtos)
                {
                    if (!IsValid(movieDto))
                    {
                        continue;
                    }

                    Guid movieGuid = Guid.Empty;
                    if (!IsGuidValid(movieDto.Id, ref movieGuid))
                    {
                        continue;
                    }

                    bool isReleaseDateValid = DateTime
                        .TryParse(movieDto.ReleaseDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);
                    if (!isReleaseDateValid)
                    {
                        continue;
                    }

                    if (allMovies.Any(
                            m => m.Id.ToString().ToLowerInvariant() == movieGuid.ToString().ToLowerInvariant()))
                    {
                        continue;
                    }

                    Movie movie = AutoMapperConfig.MapperInstance.Map<Movie>(movieDto);
                    movie.ReleaseDate = releaseDate;

                    await dbContext.Movies.AddAsync(movie);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while seeding the movies in the database!");
            }
        }

        private static bool IsValid(object obj)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            var context = new ValidationContext(obj);
            var isValid = Validator.TryValidateObject(obj, context, validationResults);

            return isValid;
        }

        private static bool IsGuidValid(string id, ref Guid parsedGuid)
        {
            // Non-existing parameter in the URL
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            // Invalid parameter in the URL
            bool isGuidValid = Guid.TryParse(id, out parsedGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
