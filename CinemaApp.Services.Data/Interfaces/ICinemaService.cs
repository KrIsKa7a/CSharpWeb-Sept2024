namespace CinemaApp.Services.Data.Interfaces
{
    using Web.ViewModels.Cinema;

    public interface ICinemaService
    {
        Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync();

        Task AddCinemaAsync(AddCinemaFormModel model);

        Task<CinemaDetailsViewModel> GetCinemaDetailsByIdAsync(Guid id);
    }
}
