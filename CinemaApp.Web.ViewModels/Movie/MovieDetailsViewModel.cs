namespace CinemaApp.Web.ViewModels.Movie
{
    using AutoMapper;

    using Data.Models;
    using Services.Mapping;

    public class MovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string Duration { get; set; } = null!;

        public string Description { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(d => d.ReleaseDate,
                    x => x.MapFrom(s => s.ReleaseDate.ToString("MMMM yyyy")));
        }
    }
}
