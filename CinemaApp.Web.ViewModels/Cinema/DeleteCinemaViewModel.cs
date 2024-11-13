namespace CinemaApp.Web.ViewModels.Cinema
{
    using Data.Models;
    using Services.Mapping;

    public class DeleteCinemaViewModel : IMapFrom<Cinema>
    {
        public string Id { get; set; } = null!;

        public string? Name { get; set; }

        public string? Location { get; set; }
    }
}
