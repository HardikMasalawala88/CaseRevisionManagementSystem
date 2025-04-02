using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Repository.Interface;
using CaseTracker.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseTracker.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId) =>
            await _userRepository.GetUserByIdAsync(userId);

        public async Task<ApplicationUser> GetLoggedInUserAsync(string username) =>
            await _userRepository.GetLoggedInUserAsync(username);

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync() =>
            await _userRepository.GetUsersAsync();

        public async Task<IdentityResult> InsertUserAsync(ApplicationUser user, string password) =>
            await _userRepository.InsertUserAsync(user, password);

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user) =>
            await _userRepository.UpdateUserAsync(user);

        public async Task<IdentityResult> DeleteUserAsync(string userId) =>
            await _userRepository.DeleteUserAsync(userId);

        public async Task SoftDeleteUserAsync(string userId) =>
            await _userRepository.SoftDeleteUserAsync(userId);

        public async Task ActivateUserAsync(string userId) =>
            await _userRepository.ActivateUserAsync(userId);

        public async Task<bool> UserExistsByEmailAsync(string email) =>
            await _userRepository.UserExistsByEmailAsync(email);

        public async Task<ApplicationUser> GetUserByEmailAsync(string email) =>
            await _userRepository.GetUserByEmailAsync(email);

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword) =>
            await _userRepository.ChangePasswordAsync(userId, currentPassword, newPassword);

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword) =>
            await _userRepository.ResetPasswordAsync(userId, token, newPassword);

        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role) =>
            await _userRepository.GetUsersByRoleAsync(role);

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName) =>
            await _userRepository.AddUserToRoleAsync(userId, roleName);

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName) =>
            await _userRepository.RemoveUserFromRoleAsync(userId, roleName);

        public async Task<IList<string>> GetUserRolesAsync(string userId) =>
            await _userRepository.GetUserRolesAsync(userId);
    }

    //public class UserService : IUserService
    //{
    //    private readonly IUserRepository _userRepository;
    //    private readonly ApplicationContext _context;

    //    public UserService(IUserRepository userRepository, ApplicationContext context)
    //    {
    //        _userRepository = userRepository;
    //        _context = context;
    //    }

    //    public async Task<UserFM> CreateOrUpdateUser(UserFM userFM)
    //    {
    //        try
    //        {
    //            var userDetails = _userRepository.GetUserByIdAsync(userFM.Id.ToString());
    //            ////var userDetails = _context.UserData.Where(x => x.Id == userFM.Id).FirstOrDefault();
    //            //if(userDetails != null)
    //            //{
    //            //    //User user = new User();
    //            //    //user.Id = userDetails.Id;
    //            //    //userDetails.Name = userFM.Name;
    //            //    userDetails.Email = userFM.EmailId;
    //            //    userDetails.Gender = userFM.Gender;
    //            //    userDetails.Address = userFM.Address;
    //            //    userDetails.City = userFM.City;
    //            //    userDetails.MobileNo = userFM.MobileNo;
    //            //    userDetails.Role = userFM.Role;
    //            //    userDetails.Username = userFM.Username;
    //            //    userDetails.Password = userFM.Password;
    //            //    userDetails.ModifiedBy = userFM.Name;
    //            //    userDetails.ModifiedDate = DateTime.UtcNow;
    //            //   await _userRepository.UpdateUserAsync(userDetails);
    //            //}
    //            //else
    //            //{
    //            //    User user = new User();
    //            //    user.Name = userFM.Name;
    //            //    user.Email = userFM.EmailId;
    //            //    user.Gender = userFM.Gender;
    //            //    user.Address = userFM.Address;
    //            //    user.City = userFM.City;
    //            //    user.MobileNo = userFM.MobileNo;
    //            //    user.Role = userFM.Role;
    //            //    user.Username = userFM.Username;
    //            //    user.Password = userFM.Password;
    //            //    user.CreatedBy = userFM.Name;

    //            //    await _userRepository.InsertUserAsync(user);
    //            //    userFM.Id = user.Id;
    //            //}
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //        return userFM;
    //    }

    //    public async Task<ApplicationUser> GetUserById(string userId)
    //    {
    //        //ApplicationUser userInfo = _userRepository.GetUsers().FirstOrDefault(x => x.Id == userId && !x.IsDelete);
    //        ApplicationUser userInfo = await _userRepository.GetUserByIdAsync(userId);
    //        return userInfo;
    //    }

    //    //public ApplicationUser GetUserDetails(LoginFM loginUser)
    //    //{
    //    //    ApplicationUser userInfo = _userRepository.GetUsers().Where(x => x.Username == loginUser.Username &&
    //    //                                            x.Password == loginUser.Password).FirstOrDefault();
    //    //    return userInfo;
    //    //}


    //    //public IList<string> GetUserRole(ApplicationUser user)
    //    //{
    //    //    IList<string> userRole = _userRepository.GetUsers().Where(x => x.Username == user.Username &&
    //    //                                            x.Password == user.Password).Select(y => y.Role).ToList();
    //    //    return userRole;
    //    //}
    //}
}
