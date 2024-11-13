namespace CinemaApp.Common
{
    public static class EntityValidationMessages
    {
        public static class Movie
        {
            public const string TitleRequiredMessage = "Movie title is required.";
            public const string GenreRequiredMessage = "Genre is required.";
            public const string ReleaseDateRequiredMessage = "Release date is required in format MM/yyyy";
            public const string DirectorRequiredMessage = "Director name is required.";
            public const string DurationRequiredMessage = "Please specify movie duration.";
        }

        public static class CinemaMovie
        {
            public const string AvailableTicketsRequiredMessage = "Please enter the number of available tickets.";
            public const string AvailableTicketsRangeMessage = "Available tickets must be a positive number.";
        }

        public static class Ticket
        {
            public const string IncorrectPriceMessage = "Ticket price should be positive!";
            public const string IncorrectCountMessage = "Tickets count should be positive number!";
        }
    }
}
