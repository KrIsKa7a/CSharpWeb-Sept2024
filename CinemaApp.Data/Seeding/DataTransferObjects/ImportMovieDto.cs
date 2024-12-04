namespace CinemaApp.Data.Seeding.DataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Models;
    using Services.Mapping;

    using static Common.EntityValidationConstants.Movie;

    public class ImportMovieDto : IMapTo<Movie>, IHaveCustomMappings
    {
        [Required]
        [MinLength(IdMinLength)]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GenreMinLength)]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Required]
        public string ReleaseDate { get; set; } = null!;

        [Required]
        [MinLength(DirectorNameMinLength)]
        [MaxLength(DirectorNameMaxLength)]
        public string Director { get; set; } = null!;

        [Range(DurationMinValue, DurationMaxValue)]
        public int Duration { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ImportMovieDto, Movie>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => Guid.Parse(s.Id)))
                .ForMember(d => d.ReleaseDate, opt => opt.Ignore());
        }
    }
}
