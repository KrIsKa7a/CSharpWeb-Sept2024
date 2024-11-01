namespace CinemaApp.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using Mapping;
    using Web.ViewModels.Watchlist;

    using static Common.EntityValidationConstants.Movie;

    public class WatchlistService : BaseService, IWatchlistService
    {
        private readonly IRepository<ApplicationUserMovie, object> userMovieRepository;
        private readonly IRepository<Movie, Guid> movieRepository;

        public WatchlistService(IRepository<ApplicationUserMovie, object> userMovieRepository, 
            IRepository<Movie, Guid> movieRepository)
        {
            this.userMovieRepository = userMovieRepository;
            this.movieRepository = movieRepository;
        }

        public async Task<IEnumerable<ApplicationUserWatchlistViewModel>> GetUserWatchListByUserIdAsync(string userId)
        {
            IEnumerable<ApplicationUserWatchlistViewModel> watchlist = await this.userMovieRepository
                .GetAllAttached()
                .Include(um => um.Movie)
                .Where(um => um.ApplicationUserId.ToString().ToLower() == userId.ToLower())
                .To<ApplicationUserWatchlistViewModel>()
                .ToListAsync();

            return watchlist;
        }

        public async Task<bool> AddMovieToUserWatchListAsync(string? movieId, string userId)
        {
            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(movieId, ref movieGuid))
            {
                return false;
            }

            Movie? movie = await this.movieRepository
                .GetByIdAsync(movieGuid);
            if (movie == null)
            {
                return false;
            }

            Guid userGuid = Guid.Parse(userId);

            ApplicationUserMovie? addedToWatchlistAlready = await this.userMovieRepository
                .FirstOrDefaultAsync(um => um.MovieId == movieGuid &&
                                           um.ApplicationUserId == userGuid);
            if (addedToWatchlistAlready == null)
            {
                ApplicationUserMovie newUserMovie = new ApplicationUserMovie()
                {
                    ApplicationUserId = userGuid,
                    MovieId = movieGuid
                };

                await this.userMovieRepository.AddAsync(newUserMovie);
            }

            return true;
        }

        public async Task<bool> RemoveMovieFromUserWatchListAsync(string? movieId, string userId)
        {
            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(movieId, ref movieGuid))
            {
                return false;
            }

            Movie? movie = await this.movieRepository
                .GetByIdAsync(movieGuid);
            if (movie == null)
            {
                return false;
            }

            Guid userGuid = Guid.Parse(userId);

            // TODO: Implement Soft-Delete
            ApplicationUserMovie? applicationUserMovie = await this.userMovieRepository
                .FirstOrDefaultAsync(um => um.MovieId == movieGuid &&
                                           um.ApplicationUserId == userGuid);
            if (applicationUserMovie != null)
            {
                await this.userMovieRepository.DeleteAsync(applicationUserMovie);
            }

            return true;
        }
    }
}
