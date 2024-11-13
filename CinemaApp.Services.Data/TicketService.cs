namespace CinemaApp.Services.Data
{
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using Mapping;
    using Web.ViewModels.CinemaMovie;

    public class TicketService : BaseService, ITicketService
    {
        private readonly IRepository<CinemaMovie, object> cinemaMovieRepository;

        public TicketService(IRepository<CinemaMovie, object> cinemaMovieRepository)
        {
            this.cinemaMovieRepository = cinemaMovieRepository;
        }

        public async Task<bool> SetAvailableTicketsAsync(SetAvailableTicketsViewModel model)
        {
            CinemaMovie cinemaMovie = AutoMapperConfig.MapperInstance
                .Map<CinemaMovie>(model);

            return await this.cinemaMovieRepository.UpdateAsync(cinemaMovie);
        }
    }
}
