namespace CinemaApp.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using Mapping;
    using Web.ViewModels.Cinema;

    public class CinemaService : ICinemaService
    {
        private readonly IRepository<Cinema, Guid> cinemaRepository;

        public CinemaService(IRepository<Cinema, Guid> cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync()
        {
            IEnumerable<CinemaIndexViewModel> cinemas = await this.cinemaRepository
                .GetAllAttached()
                .OrderBy(c => c.Location)
                .To<CinemaIndexViewModel>()
                .ToArrayAsync();

            return cinemas;
        }

        public Task AddCinemaAsync(AddCinemaFormModel model)
        {
            throw new NotImplementedException();
        }

        public Task<CinemaDetailsViewModel> GetCinemaDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
