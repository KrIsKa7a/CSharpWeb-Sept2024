namespace CinemaApp.Web.ViewModels.Ticket
{
    using AutoMapper;

    using Data.Models;
    using Services.Mapping;

    public class UserTicketViewModel : IMapFrom<Ticket>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;

        public string MovieTitle { get; set; } = null!;

        public string CinemaName { get; set; } = null!;

        public string CinemaLocation { get; set; } = null!;

        public string Price { get; set; } = null!;


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Ticket, UserTicketViewModel>()
                .ForMember(d => d.MovieTitle,
                    opt => opt.MapFrom(s => s.Movie.Title))
                .ForMember(d => d.CinemaName, opt => opt.MapFrom(s => s.Cinema.Name))
                .ForMember(d => d.CinemaLocation, opt => opt.MapFrom(s => s.Cinema.Location))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price.ToString("F2")));
        }
    }
}
