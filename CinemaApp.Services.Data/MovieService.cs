namespace CinemaApp.Services.Data
{
    using System.Globalization;

    using Microsoft.EntityFrameworkCore;

    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using Mapping;
    using Web.ViewModels.Cinema;
    using Web.ViewModels.CinemaMovie;
    using Web.ViewModels.Movie;

    using static Common.EntityValidationConstants.Movie;
    using static Common.ApplicationConstants;

    public class MovieService : BaseService, IMovieService
    {
        private readonly IRepository<Movie, Guid> movieRepository;
        private readonly IRepository<Cinema, Guid> cinemaRepository;
        private readonly IRepository<CinemaMovie, object> cinemaMovieRepository;

        public MovieService(IRepository<Movie, Guid> movieRepository, 
            IRepository<Cinema, Guid> cinemaRepository,
            IRepository<CinemaMovie, object> cinemaMovieRepository)
        {
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.cinemaMovieRepository = cinemaMovieRepository;
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            return await movieRepository
                .GetAllAttached()
                .To<AllMoviesIndexViewModel>()
                .ToArrayAsync();
        }

        public async Task<bool> AddMovieAsync(AddMovieInputModel inputModel)
        {
            bool isReleaseDateValid = DateTime
                .TryParseExact(inputModel.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out DateTime releaseDate);
            if (!isReleaseDateValid)
            {
                return false;
            }

            Movie movie = new Movie();
            AutoMapperConfig.MapperInstance.Map(inputModel, movie);
            movie.ReleaseDate = releaseDate;

            await this.movieRepository.AddAsync(movie);

            return true;
        }

        public async Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id)
        {
            Movie? movie = await this.movieRepository
                .GetByIdAsync(id);
            MovieDetailsViewModel? viewModel = new MovieDetailsViewModel();
            if (movie != null)
            {
                AutoMapperConfig.MapperInstance.Map(movie, viewModel);
            }

            return viewModel;
        }

        public async Task<AddMovieToCinemaInputModel?> GetAddMovieToCinemaInputModelByIdAsync(Guid id)
        {
            Movie? movie = await this.movieRepository
                .GetByIdAsync(id);
            AddMovieToCinemaInputModel? viewModel = null;
            if (movie != null)
            {
                viewModel = new AddMovieToCinemaInputModel()
                {
                    Id = id.ToString(),
                    MovieTitle = movie.Title,
                    Cinemas = await this.cinemaRepository
                        .GetAllAttached()
                        .Include(c => c.CinemaMovies)
                        .ThenInclude(cm => cm.Movie)
                        .Where(c => c.IsDeleted == false)
                        .Select(c => new CinemaCheckBoxItemInputModel()
                        {
                            Id = c.Id.ToString(),
                            Name = c.Name,
                            Location = c.Location,
                            IsSelected = c.CinemaMovies
                                .Any(cm => cm.Movie.Id == id &&
                                           cm.IsDeleted == false)
                        })
                        .ToArrayAsync()
                };
            }

            return viewModel;
        }

        public async Task<bool> AddMovieToCinemasAsync(Guid movieId, AddMovieToCinemaInputModel model)
        {
            Movie? movie = await this.movieRepository
                .GetByIdAsync(movieId);
            if (movie == null)
            {
                return false;
            }

            ICollection<CinemaMovie> entitiesToAdd = new List<CinemaMovie>();
            foreach (CinemaCheckBoxItemInputModel cinemaInputModel in model.Cinemas)
            {
                Guid cinemaGuid = Guid.Empty;
                bool isCinemaGuidValid = this.IsGuidValid(cinemaInputModel.Id, ref cinemaGuid);
                if (!isCinemaGuidValid)
                {
                    return false;
                }

                Cinema? cinema = await this.cinemaRepository
                    .GetByIdAsync(cinemaGuid);
                if (cinema == null || cinema.IsDeleted)
                {
                    return false;
                }

                CinemaMovie? cinemaMovie = await this.cinemaMovieRepository
                    .FirstOrDefaultAsync(cm => cm.MovieId == movieId &&
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
            }

            await this.cinemaMovieRepository.AddRangeAsync(entitiesToAdd.ToArray());

            return true;
        }

        public async Task<EditMovieFormModel?> GetEditMovieFormModelByIdAsync(Guid id)
        {
            // TODO: Check soft delete
            EditMovieFormModel? editMovieFormModel = await this.movieRepository
                .GetAllAttached()
                .To<EditMovieFormModel>()
                .FirstOrDefaultAsync(m => m.Id.ToLower() == id.ToString().ToLower());
            if (editMovieFormModel != null && 
                editMovieFormModel.ImageUrl.Equals(NoImageUrl))
            {
                editMovieFormModel.ImageUrl = "No image";
            }

            return editMovieFormModel;
        }

        public async Task<bool> EditMovieAsync(EditMovieFormModel formModel)
        {
            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(formModel.Id, ref movieGuid))
            {
                return false;
            }

            Movie edittedMovie = AutoMapperConfig.MapperInstance.Map<Movie>(formModel);
            edittedMovie.Id = movieGuid;

            bool isReleaseDateValid = DateTime.TryParseExact(formModel.ReleaseDate, ReleaseDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);
            if (!isReleaseDateValid)
            {
                return false;
            }

            edittedMovie.ReleaseDate = releaseDate;

            if (formModel.ImageUrl == null ||
                formModel.ImageUrl.Equals("No image"))
            {
                edittedMovie.ImageUrl = NoImageUrl;
            }

            return await this.movieRepository.UpdateAsync(edittedMovie);
        }

        public async Task<AvailableTicketsViewModel?> GetAvailableTicketsByIdAsync(Guid cinemaId, Guid movieId)
        {
            CinemaMovie? cinemaMovie = await this.cinemaMovieRepository
                .FirstOrDefaultAsync(cm => cm.MovieId == movieId &&
                                                     cm.CinemaId == cinemaId);

            AvailableTicketsViewModel availableTicketsViewModel = null;
            if (cinemaMovie != null)
            {
                availableTicketsViewModel = new AvailableTicketsViewModel()
                {
                    CinemaId = cinemaId.ToString(),
                    MovieId = movieId.ToString(),
                    Quantity = 0,
                    AvailableTickets = cinemaMovie.AvailableTickets
                };
            }

            return availableTicketsViewModel;
        }
    }
}
