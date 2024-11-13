namespace CinemaApp.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Services.Mapping;

    using static Common.EntityValidationConstants.Ticket;
    using static Common.EntityValidationMessages.Ticket;

    public class BuyTicketViewModel : IMapTo<Ticket>
    {
        [Required]
        public string CinemaId { get; set; } = null!;

        [Required]
        public string MovieId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue, ErrorMessage = IncorrectPriceMessage)]
        public decimal Price { get; set; }

        [Range(CountMinValue, CountMaxValue, ErrorMessage = IncorrectCountMessage)]
        public int Count { get; set; }
    }
}
