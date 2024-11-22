namespace CinemaApp.Services.Data.Interfaces
{
    using CinemaApp.Web.ViewModels.CinemaMovie;

    using Web.ViewModels.Movie;

    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync();

        Task<bool> AddMovieAsync(AddMovieInputModel inputModel);

        Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id);

        Task<AddMovieToCinemaInputModel?> GetAddMovieToCinemaInputModelByIdAsync(Guid id);

        Task<bool> AddMovieToCinemasAsync(Guid movieId, AddMovieToCinemaInputModel model);

        Task<EditMovieFormModel?> GetEditMovieFormModelByIdAsync(Guid id);

        Task<bool> EditMovieAsync(EditMovieFormModel formModel);

        Task<AvailableTicketsViewModel?> GetAvailableTicketsByIdAsync(Guid cinemaId, Guid movieId);
    }
}
