namespace CinemaApp.Web.ViewModels.Cinema
{
    using Data.Models;
    using Services.Mapping;

    public class CinemaIndexViewModel : IMapFrom<Cinema>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;
    }
}
