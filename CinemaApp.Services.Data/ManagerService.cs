namespace CinemaApp.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;

    public class ManagerService : BaseService, IManagerService
    {
        private readonly IRepository<Manager, Guid> managersRepository;

        public ManagerService(IRepository<Manager, Guid> managersRepository)
        {
            this.managersRepository = managersRepository;
        }

        public async Task<bool> IsUserManagerAsync(string? userId)
        {
            // Not a valid use-case, but we write defensive programming
            if (String.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            bool result = await this.managersRepository
                .GetAllAttached()
                .AnyAsync(m => m.UserId.ToString().ToLower() == userId);

            return result;
        }
    }
}
