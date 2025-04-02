using CaseTracker.Data.ContextModels;
using CaseTracker.Repository.Interface;

namespace CaseTracker.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // Get a user by ID
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        // Get a user by username
        public async Task<ApplicationUser> GetLoggedInUserAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        // Get all users
        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        // Insert a new user
        public async Task<IdentityResult> InsertUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        // Update user information
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        // Delete user permanently
        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        // Soft delete (Deactivate) user
        public async Task SoftDeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Address += " (Deactivated)";
                user.UpdatedDate = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
            }
        }

        // Reactivate (Enable) user
        public async Task ActivateUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.Address.Contains("(Deactivated)"))
            {
                user.Address = user.Address.Replace(" (Deactivated)", "");
                user.UpdatedDate = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
            }
        }

        // Check if user exists by email
        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email);
        }

        // Get user by email
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        // Change user password
        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        // Reset password (Admin/Support use case)
        public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.ResetPasswordAsync(user, token, newPassword);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        // Get users by role
        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            return users;
        }

        // Assign a role to a user
        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }

        // Remove a user from a role
        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        // Get roles for a user
        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.GetRolesAsync(user);
            }
            return new List<string>();
        }
    }

    //public class UserRepository : IUserRepository
    //{
    //    private readonly IRepository<ApplicationUser> _userRepository;
    //    public UserRepository(IRepository<User> userRepository)
    //    {
    //        _userRepository = userRepository;
    //    }
    //    public void DeleteUser(long id)
    //    {
    //        User user = GetUser(id);
    //        user.IsDelete = true;
    //        user.ModifiedDate = DateTime.UtcNow;
    //        user.ModifiedBy = user.CreatedBy;
    //        _userRepository.SaveChanges();
    //    }

    //    public User GetLoggedInUser(string username)
    //    {
    //        return _userRepository.GetByUsername(username);
    //    }

    //    public User GetUser(long id)
    //    {
    //        return _userRepository.GetById(id);
    //    }

    //    public IEnumerable<User> GetUsers()
    //    {
    //        return _userRepository.GetAll();
    //    }

    //    public User InsertUser(User user)
    //    {
    //        User userData = _userRepository.Insert(user);
    //        return userData;
    //    }

    //    public void UpdateUser(User user)
    //    {
    //        _userRepository.Update(user);
    //    }
    //}
}
