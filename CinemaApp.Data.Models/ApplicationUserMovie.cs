namespace CinemaApp.Data.Models
{
    public class ApplicationUserMovie
    {
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        public Guid MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;
    }
}
