namespace CinemaApp.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using Mapping;
    using Web.ViewModels.Cinema;
    using Web.ViewModels.Movie;

    public class CinemaService : BaseService, ICinemaService
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
                .Where(c => c.IsDeleted == false)
                .OrderBy(c => c.Location)
                .To<CinemaIndexViewModel>()
                .ToArrayAsync();

            return cinemas;
        }

        public async Task AddCinemaAsync(AddCinemaFormModel model)
        {
            Cinema cinema = new Cinema();
            AutoMapperConfig.MapperInstance.Map(model, cinema);

            await this.cinemaRepository.AddAsync(cinema);
        }

        public async Task<CinemaDetailsViewModel?> GetCinemaDetailsByIdAsync(Guid id)
        {
            Cinema? cinema = await this.cinemaRepository
                .GetAllAttached()
                .Include(c => c.CinemaMovies)
                .ThenInclude(cm => cm.Movie)
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);
            
            CinemaDetailsViewModel? viewModel = null;
            if (cinema != null)
            {
                viewModel = new CinemaDetailsViewModel()
                {
                    Id = cinema.Id.ToString(),
                    Name = cinema.Name,
                    Location = cinema.Location,
                    Movies = cinema.CinemaMovies
                        .Where(cm => cm.IsDeleted == false)
                        .Select(cm => new CinemaMovieViewModel()
                        {
                            Id = cm.Movie.Id.ToString(),
                            Title = cm.Movie.Title,
                            Genre = cm.Movie.Genre,
                            Duration = cm.Movie.Duration,
                            Description = cm.Movie.Description,
                            AvailableTickets = cm.AvailableTickets
                        })
                        .ToArray()
                };
            }

            return viewModel;
        }

        public async Task<EditCinemaFormModel?> GetCinemaForEditByIdAsync(Guid id)
        {
            EditCinemaFormModel? cinemaModel = await this.cinemaRepository
                .GetAllAttached()
                .Where(c => c.IsDeleted == false)
                .To<EditCinemaFormModel>()
                .FirstOrDefaultAsync(c => c.Id.ToLower() == id.ToString().ToLower());

            return cinemaModel;
        }

        public async Task<bool> EditCinemaAsync(EditCinemaFormModel model)
        {
            Cinema cinemaEntity = AutoMapperConfig.MapperInstance
                .Map<EditCinemaFormModel, Cinema>(model);

            bool result = await this.cinemaRepository.UpdateAsync(cinemaEntity);
            return result;
        }

        public async Task<DeleteCinemaViewModel?> GetCinemaForDeleteByIdAsync(Guid id)
        {
            DeleteCinemaViewModel? cinemaToDelete = await this.cinemaRepository
                .GetAllAttached()
                .Where(c => c.IsDeleted == false)
                .To<DeleteCinemaViewModel>()
                .FirstOrDefaultAsync(c => c.Id.ToLower() == id.ToString().ToLower());

            return cinemaToDelete;
        }

        public async Task<bool> SoftDeleteCinemaAsync(Guid id)
        {
            Cinema? cinemaToDelete = await this.cinemaRepository
                .FirstOrDefaultAsync(c => c.Id.ToString().ToLower() == id.ToString().ToLower());
            if (cinemaToDelete == null)
            {
                return false;
            }

            cinemaToDelete.IsDeleted = true;
            return await this.cinemaRepository.UpdateAsync(cinemaToDelete);
        }
    }
}
