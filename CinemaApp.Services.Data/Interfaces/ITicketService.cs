namespace CinemaApp.Services.Data.Interfaces
{
    using Web.ViewModels.CinemaMovie;

    public interface ITicketService
    {
        public Task<bool> SetAvailableTicketsAsync(SetAvailableTicketsViewModel model);
    }
}
