namespace CinemaApp.Services.Data.Interfaces
{
    public interface IManagerService
    {
        Task<bool> IsUserManagerAsync(string? userId);
    }
}
