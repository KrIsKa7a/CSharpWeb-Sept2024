namespace CinemaApp.Web.ViewModels.CinemaMovie
{
    public class AvailableTicketsViewModel
    {
        public string CinemaId { get; set; } = null!;

        public string MovieId { get; set; } = null!;

        public int Quantity { get; set; }

        public int AvailableTickets { get; set; }
    }
}
