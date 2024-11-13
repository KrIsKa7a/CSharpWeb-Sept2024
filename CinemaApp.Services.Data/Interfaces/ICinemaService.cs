namespace CinemaApp.Services.Data.Interfaces
{
    using Web.ViewModels.Cinema;

    public interface ICinemaService
    {
        Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync();

        Task AddCinemaAsync(AddCinemaFormModel model);

        Task<CinemaDetailsViewModel?> GetCinemaDetailsByIdAsync(Guid id);

        Task<EditCinemaFormModel?> GetCinemaForEditByIdAsync(Guid id);

        Task<bool> EditCinemaAsync(EditCinemaFormModel model);

        Task<DeleteCinemaViewModel?> GetCinemaForDeleteByIdAsync(Guid id);

        Task<bool> SoftDeleteCinemaAsync(Guid id);
    }
}
