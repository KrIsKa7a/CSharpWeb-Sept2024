namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Data.Models;
    using Services.Data.Interfaces;
    using System.Collections.Generic;
    using ViewModels.Watchlist;
    
    using static Common.ErrorMessages.Watchlist;

    [Authorize]
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;
        private readonly UserManager<ApplicationUser> userManager;

        public WatchlistController(IWatchlistService watchlistService, 
            IManagerService managerService, UserManager<ApplicationUser> userManager)
            : base(managerService)
        {
            this.watchlistService = watchlistService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.userManager.GetUserId(User)!;
            if (String.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            IEnumerable<ApplicationUserWatchlistViewModel> watchList =
                await this.watchlistService
                    .GetUserWatchListByUserIdAsync(userId);

            return View(watchList);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(string? movieId)
        {
            string userId = this.userManager.GetUserId(User)!;
            if (String.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.watchlistService
                .AddMovieToUserWatchListAsync(movieId, userId);
            if (result == false)
            {
                TempData[nameof(AddToWatchListNotSuccessfullMessage)] = AddToWatchListNotSuccessfullMessage;
                return this.RedirectToAction("Index", "Movie");
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(string? movieId)
        {
            string userId = this.userManager.GetUserId(User)!;
            if (String.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.watchlistService
                .RemoveMovieFromUserWatchListAsync(movieId, userId);
            if (result == false)
            {
                // TODO: Implement a way to transfer the Error Message to the View
                return this.RedirectToAction("Index", "Movie");
            }

            return this.RedirectToAction(nameof(Index));
        }
    }
}
