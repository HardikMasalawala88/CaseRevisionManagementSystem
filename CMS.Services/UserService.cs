using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Repository.Interface;
using CMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class UserService : IUserService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly ApplicationContext _context;

        public UserService(IAppUserRepository userRepository, ApplicationContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public bool CreateOrUpdateUser(ApplicationUserFM userFM)
        {
            try
            {
                var userDetails = _context.ApplicationUser.Where(x => x.Id == userFM.Id).FirstOrDefault();
                if(userDetails != null)
                {
                    ApplicationUser user = new ApplicationUser();
                    user.Id = userDetails.Id;
                    user.Name = userDetails.Name;
                    user.Email = userDetails.Email;
                    user.Gender = userDetails.Gender;
                    user.Address = userDetails.Address;
                    user.City = userDetails.City;
                    user.PhoneNumber = userDetails.PhoneNumber;
                    user.Role = userDetails.Role;
                    user.UserName = userDetails.UserName;
                    user.PasswordHash = userDetails.PasswordHash;
                    user.ModifiedBy = userDetails.Id;
                    user.ModifiedDate = DateTime.UtcNow;
                    _userRepository.UpdateUser(user);
                }
                else
                {
                    ApplicationUser user = new ApplicationUser();
                    user.Name = userFM.Name;
                    user.Email = userFM.Email;
                    user.Gender = userFM.Gender;
                    user.Address = userFM.Address;
                    user.City = userFM.City;
                    user.PhoneNumber = userFM.PhoneNumber;
                    user.Role = userFM.Role;
                    user.UserName = userFM.UserName;
                    user.PasswordHash = userFM.PasswordHash;
                    user.CreatedBy = userFM.Name;

                    _userRepository.InsertUser(user);
                    userFM.Id = user.Id;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //public ApplicationUser GetUserDetails(LoginFM loginUser)
        //{
        //    ApplicationUser userInfo = _userRepository.GetUsers().Where(x => x.Username == loginUser.Username &&
        //                                            x.Password == loginUser.Password).FirstOrDefault();
        //    return userInfo;
        //}

        //public IList<string> GetUserRole(User user)
        //{
        //    IList<string> userRole = _userRepository.GetUsers().Where(x => x.Username == user.Username &&
        //                                            x.Password == user.Password).Select(y => y.Role).ToList();
        //    return userRole;
        //}
    }
}
