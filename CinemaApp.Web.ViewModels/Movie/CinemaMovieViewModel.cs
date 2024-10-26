namespace CinemaApp.Web.ViewModels.Movie
{
    using Data.Models;
    using Services.Mapping;

    public class CinemaMovieViewModel : IMapFrom<Movie>
    {
        public string Title { get; set; } = null!;

        public int Duration { get; set; }
    }
}
