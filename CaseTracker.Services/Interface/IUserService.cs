using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Services.Interface
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetLoggedInUserAsync(string username);
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        Task<IdentityResult> InsertUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task SoftDeleteUserAsync(string userId);
        Task ActivateUserAsync(string userId);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
        Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role);
        Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName);
        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName);
        Task<IList<string>> GetUserRolesAsync(string userId);
    }
    //public interface IUserService
    //{
    //    Task<ApplicationUser> GetUserById(string userId);
    //    Task<UserFM> CreateOrUpdateUser(UserFM userFM);

    //    //ApplicationUser GetUserDetails(LoginFM loginUser);
    //    //IList<string> GetUserRole(ApplicationUser user);
    //}
}
