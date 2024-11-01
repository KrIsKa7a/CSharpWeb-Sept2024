namespace CinemaApp.Data.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public Guid CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; } = null!;

        public Guid MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;
    }
}
