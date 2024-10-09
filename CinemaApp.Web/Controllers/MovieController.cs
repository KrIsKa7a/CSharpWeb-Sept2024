namespace CinemaApp.Web.Controllers
{
    using System.Globalization;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using ViewModels.Cinema;
    using ViewModels.Movie;

    using static Common.EntityValidationConstants.Movie;

    public class MovieController : BaseController
    {
        private readonly CinemaDbContext dbContext;

        public MovieController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> allMovies = await this.dbContext
                .Movies
                .ToArrayAsync();

            return this.View(allMovies);
        }

        [HttpGet]
#pragma warning disable CS1998
        public async Task<IActionResult> Create()
#pragma warning restore CS1998
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel inputModel)
        {
            bool isReleaseDateValid = DateTime
                .TryParseExact(inputModel.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out DateTime releaseDate);
            if (!isReleaseDateValid)
            {
                // ModelState becomes invalid => IsValid = false;
                this.ModelState.AddModelError(nameof(inputModel.ReleaseDate), String.Format("The Release Date must be in the following format: {0}", ReleaseDateFormat));
            }

            if (!this.ModelState.IsValid)
            {
                // Render the same form with user entered values + model errors 
                return this.View(inputModel);
            }

            Movie movie = new Movie()
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                ReleaseDate = releaseDate,
                Director = inputModel.Director,
                Duration = inputModel.Duration,
                Description = inputModel.Description,
                ImageUrl = inputModel.ImageUrl
            };

            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                // Invalid id format
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);
            if (movie == null)
            {
                // Non-existing movie guid
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> AddToProgram(string? id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);
            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaInputModel viewModel = new AddMovieToCinemaInputModel()
            {
                Id = id!,
                MovieTitle = movie.Title,
                Cinemas = await this.dbContext
                    .Cinemas
                    .Include(c => c.CinemaMovies)
                    .ThenInclude(cm => cm.Movie)
                    .Select(c => new CinemaCheckBoxItemInputModel()
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                        Location = c.Location,
                        IsSelected = c.CinemaMovies
                            .Any(cm => cm.Movie.Id == movieGuid)
                    })
                    .ToArrayAsync()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(model.Id, ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);
            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            ICollection<CinemaMovie> entitiesToAdd = new List<CinemaMovie>();
            foreach (CinemaCheckBoxItemInputModel cinemaInputModel in model.Cinemas)
            {
                Guid cinemaGuid = Guid.Empty;
                bool isCinemaGuidValid = this.IsGuidValid(cinemaInputModel.Id, ref cinemaGuid);
                if (!isCinemaGuidValid)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid cinema selected!");
                    return this.View(model);
                }

                Cinema? cinema = await this.dbContext
                    .Cinemas
                    .FirstOrDefaultAsync(c => c.Id == cinemaGuid);
                if (cinema == null)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid cinema selected!");
                    return this.View(model);
                }

                CinemaMovie? cinemaMovie = await this.dbContext
                    .CinemasMovies
                    .FirstOrDefaultAsync(cm => cm.MovieId == movieGuid &&
                                               cm.CinemaId == cinemaGuid);
                if (cinemaInputModel.IsSelected)
                {
                    if (cinemaMovie == null)
                    {
                        entitiesToAdd.Add(new CinemaMovie()
                        {
                            Cinema = cinema,
                            Movie = movie
                        });
                    }
                    else
                    {
                        cinemaMovie.IsDeleted = false;
                    }
                }
                else
                {
                    if (cinemaMovie != null)
                    {
                        cinemaMovie.IsDeleted = true;
                    }
                }

                await this.dbContext.SaveChangesAsync();
            }

            await this.dbContext.CinemasMovies.AddRangeAsync(entitiesToAdd);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index), "Cinema");
        }
    }
}
