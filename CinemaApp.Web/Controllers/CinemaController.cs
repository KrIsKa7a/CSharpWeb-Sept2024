namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Services.Data.Interfaces;
    using ViewModels.Cinema;
    using ViewModels.Movie;

    public class CinemaController : BaseController
    {
        private readonly CinemaDbContext dbContext;
        private readonly ICinemaService cinemaService;

        public CinemaController(CinemaDbContext dbContext, ICinemaService cinemaService)
        {
            this.dbContext = dbContext;
            this.cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CinemaIndexViewModel> cinemas =
                await this.cinemaService.IndexGetAllOrderedByLocationAsync();

            return this.View(cinemas);
        }

        [HttpGet]
#pragma warning disable CS1998
        public async Task<IActionResult> Create()
#pragma warning restore CS1998
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCinemaFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Cinema cinema = new Cinema()
            {
                Name = model.Name,
                Location = model.Location
            };

            await this.dbContext.Cinemas.AddAsync(cinema);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            Guid cinemaGuid = Guid.Empty;
            bool isIdValid = this.IsGuidValid(id, ref cinemaGuid);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Cinema? cinema = await this.dbContext
                .Cinemas
                .Include(c => c.CinemaMovies)
                .ThenInclude(cm => cm.Movie)
                .FirstOrDefaultAsync(c => c.Id == cinemaGuid);

            // Invalid(non-existing) GUID in the URL
            if (cinema == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            CinemaDetailsViewModel viewModel = new CinemaDetailsViewModel()
            {
                Name = cinema.Name,
                Location = cinema.Location,
                Movies = cinema.CinemaMovies
                    .Where(cm => cm.IsDeleted == false)
                    .Select(cm => new CinemaMovieViewModel()
                    {
                        Title = cm.Movie.Title,
                        Duration = cm.Movie.Duration,
                    })
                    .ToArray()
            };

            return this.View(viewModel);
        }
    }
}
