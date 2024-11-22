namespace CinemaApp.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using Web.Infrastructure.Attributes;
    using Web.ViewModels.Cinema;
    using Web.ViewModels.CinemaMovie;
    using Web.ViewModels.Ticket;

    [ApiController]
    [Route("[controller]/")]
    public class TicketApiController : ControllerBase
    {
        private readonly ICinemaService cinemaService;
        private readonly ITicketService ticketService;
        private readonly IMovieService movieService;

        public TicketApiController(ICinemaService cinemaService,
            ITicketService ticketService, IMovieService movieService)
        {
            this.cinemaService = cinemaService;
            this.ticketService = ticketService;
            this.movieService = movieService;
        }

        [HttpGet("[action]/{id?}")]
        [ManagerOnly]
        [ProducesResponseType(typeof(CinemaDetailsViewModel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMoviesByCinema(string? id)
        {
            Guid cinemaGuid = Guid.Empty;
            if (!this.IsGuidValid(id, ref cinemaGuid))
            {
                return this.BadRequest();
            }

            CinemaDetailsViewModel? viewModel = await this.cinemaService
                .GetCinemaDetailsByIdAsync(cinemaGuid);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.Ok(viewModel);
        }

        [HttpPost("[action]")]
        [ManagerOnly]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAvailableTickets([FromBody] SetAvailableTicketsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            bool isSuccess = await this.ticketService.SetAvailableTicketsAsync(model);
            if (!isSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok("Ticket availability updated successfully!");
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AvailableTicketsViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTicketsAvailability([FromBody] GetAvailableTicketsFormModel buyTicketsModel)
        {
            Guid cinemaGuid = Guid.Empty;
            if (!this.IsGuidValid(buyTicketsModel.CinemaId, ref cinemaGuid))
            {
                return this.BadRequest();
            }

            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(buyTicketsModel.MovieId, ref movieGuid))
            {
                return this.BadRequest();
            }

            AvailableTicketsViewModel? availableTicketsViewModel = await this.movieService
                .GetAvailableTicketsByIdAsync(cinemaGuid, movieGuid);
            if (availableTicketsViewModel == null)
            {
                return this.BadRequest();
            }

            return this.Ok(availableTicketsViewModel);
        }

        protected bool IsGuidValid(string? id, ref Guid parsedGuid)
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
