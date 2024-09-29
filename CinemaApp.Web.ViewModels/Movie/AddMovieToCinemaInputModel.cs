namespace CinemaApp.Web.ViewModels.Movie
{
    using System.ComponentModel.DataAnnotations;

    using Cinema;

    using static Common.EntityValidationConstants.Movie;

    public class AddMovieToCinemaInputModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(TitleMaxLength)]
        public string MovieTitle { get; set; } = null!;

        public IList<CinemaCheckBoxItemInputModel> Cinemas { get; set; }
            = new List<CinemaCheckBoxItemInputModel>();
    }
}
