namespace CinemaApp.Data.Models
{
    public class Manager
    {
        public Guid Id { get; set; }

        public string WorkPhoneNumber { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
