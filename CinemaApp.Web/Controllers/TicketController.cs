namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.CinemaMovie;

    public class TicketController : BaseController
    {
        public TicketController(IManagerService managerService)
            : base(managerService)
        {
            
        }

        [HttpPost]
        [Authorize]
        public IActionResult BuyTickets(AvailableTicketsViewModel viewModel)
        {
            // TODO: Implement logic for buying tickets by the user
            return View();
        }
    }
}
