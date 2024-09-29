namespace CinemaApp.Web.ViewModels.Cinema
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Cinema;

    public class AddCinemaFormModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(LocationMinLength)]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;
    }
}
