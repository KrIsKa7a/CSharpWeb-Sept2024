namespace CinemaApp.Data.Models
{
    public class CinemaMovie
    {
        public Guid MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;

        public Guid CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
