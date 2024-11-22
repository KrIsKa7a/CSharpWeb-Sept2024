namespace CinemaApp.Services.Data.Interfaces
{
    using Web.ViewModels.Admin.UserManagement;

    public interface IUserService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();

        Task<bool> UserExistsByIdAsync(Guid userId);

        Task<bool> AssignUserToRoleAsync(Guid userId, string roleName);

        Task<bool> RemoveUserRoleAsync(Guid userId, string roleName);

        Task<bool> DeleteUserAsync(Guid userId);
    }
}
