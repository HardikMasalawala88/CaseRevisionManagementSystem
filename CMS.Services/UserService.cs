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
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserFM Create(UserFM userFM)
        {
            try
            {
                User user = new User();
                user.Name = userFM.Name;
                user.EmailId = userFM.EmailId;
                user.Gender = userFM.Gender;
                user.Address = userFM.Address;
                user.City = userFM.City;
                user.MobileNo = userFM.MobileNo;
                user.Role = userFM.Role;
                user.Username = userFM.Username;
                user.Password = userFM.Password;
                _userRepository.InsertUser(user);
                userFM.Id = user.Id;
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

        public IList<string> GetUserRole(User user)
        {
            IList<string> userRole = _userRepository.GetUsers().Where(x => x.Username == user.Username &&
                                                    x.Password == user.Password).Select(y => y.Role).ToList();
            return userRole;
        }
    }
}
