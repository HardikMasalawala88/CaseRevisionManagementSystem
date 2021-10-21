using CMS.Data.ContextModels;
using CMS.Repository.Interface;
using CMS.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _userRepository;
        public UserRepository(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public void DeleteUser(long id)
        {
            User user = GetUser(id);
            user.IsDelete = true;
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = user.CreatedBy;
            //_userRepository.Remove(user);
            _userRepository.SaveChanges();
        }

        public User GetUser(long id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public User InsertUser(User user)
        {
            User userData = _userRepository.Insert(user);
            return userData;
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
