using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Repository.Interface;
using CMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationContext _context;

        public UserService(IUserRepository userRepository, ApplicationContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public UserFM CreateOrUpdateUser(UserFM userFM)
        {
            try
            {
                var userDetails = _context.UserData.Where(x => x.Id == userFM.Id).FirstOrDefault();
                if(userDetails != null)
                {
                    User user = new User();
                    user.Id = userDetails.Id;
                    user.Name = userDetails.Name;
                    user.Email = userDetails.Email;
                    user.Gender = userDetails.Gender;
                    user.Address = userDetails.Address;
                    user.City = userDetails.City;
                    user.MobileNo = userDetails.MobileNo;
                    user.Role = userDetails.Role;
                    user.Username = userDetails.Username;
                    user.Password = userDetails.Password;
                    user.ModifiedBy = userDetails.Name;
                    user.ModifiedDate = DateTime.UtcNow;
                    _userRepository.UpdateUser(user);
                }
                else
                {
                    User user = new User();
                    user.Name = userFM.Name;
                    user.Email = userFM.EmailId;
                    user.Gender = userFM.Gender;
                    user.Address = userFM.Address;
                    user.City = userFM.City;
                    user.MobileNo = userFM.MobileNo;
                    user.Role = userFM.Role;
                    user.Username = userFM.Username;
                    user.Password = userFM.Password;
                    user.CreatedBy = userFM.Name;

                    _userRepository.InsertUser(user);
                    userFM.Id = user.Id;
                }
            }
            catch (Exception ex)
            {

            }
            return userFM;
        }

        public User GetUserDetails(LoginFM loginUser)
        {
            User userInfo = _userRepository.GetUsers().Where(x => x.Username == loginUser.Username &&
                                                    x.Password == loginUser.Password).FirstOrDefault();
            return userInfo;
        }
        
        public User GetUserById(long userId)
        {
            User userInfo = _userRepository.GetUsers().FirstOrDefault(x => x.Id == userId && !x.IsDelete);
            return userInfo;
        }

        public IList<string> GetUserRole(User user)
        {
            IList<string> userRole = _userRepository.GetUsers().Where(x => x.Username == user.Username &&
                                                    x.Password == user.Password).Select(y => y.Role).ToList();
            return userRole;
        }
    }
}
