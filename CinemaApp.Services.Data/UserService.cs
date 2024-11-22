namespace CinemaApp.Services.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using CinemaApp.Data.Models;
    using Interfaces;
    using Web.ViewModels.Admin.UserManagement;

    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
            IEnumerable<ApplicationUser> allUsers = await this.userManager.Users
                .ToArrayAsync();
            ICollection<AllUsersViewModel> allUsersViewModel = new List<AllUsersViewModel>();

            foreach (ApplicationUser user in allUsers)
            {
                IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);

                allUsersViewModel.Add(new AllUsersViewModel()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Roles = roles
                });
            }

            return allUsersViewModel;
        }

        public async Task<bool> UserExistsByIdAsync(Guid userId)
        {
            ApplicationUser? user = await this.userManager
                .FindByIdAsync(userId.ToString());

            return user != null;
        }

        public async Task<bool> AssignUserToRoleAsync(Guid userId, string roleName)
        {
            ApplicationUser? user = await userManager
                .FindByIdAsync(userId.ToString());
            bool roleExists = await this.roleManager.RoleExistsAsync(roleName);

            if (user == null || !roleExists)
            {
                return false;
            }

            bool alreadyInRole = await this.userManager.IsInRoleAsync(user, roleName);
            if (!alreadyInRole)
            {
                IdentityResult? result = await this.userManager
                    .AddToRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> RemoveUserRoleAsync(Guid userId, string roleName)
        {
            ApplicationUser? user = await userManager
                .FindByIdAsync(userId.ToString());
            bool roleExists = await this.roleManager.RoleExistsAsync(roleName);

            if (user == null || !roleExists)
            {
                return false;
            }

            bool alreadyInRole = await this.userManager.IsInRoleAsync(user, roleName);
            if (alreadyInRole)
            {
                IdentityResult? result = await this.userManager
                    .RemoveFromRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            ApplicationUser? user = await userManager
                .FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return false;
            }

            IdentityResult? result = await this.userManager
                .DeleteAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
