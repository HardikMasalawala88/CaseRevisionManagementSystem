using CaseTracker.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Repository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public interface IUserRepository
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

    //public interface IUserRepository
    //{
    //    IEnumerable<User> GetUsers();
    //    User GetUser(long id);
    //    User GetLoggedInUser(string username);
    //    User InsertUser(User user);
    //    void UpdateUser(User user);
    //    void DeleteUser(long id);
    //}
}
