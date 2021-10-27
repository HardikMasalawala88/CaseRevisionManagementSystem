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
    public class AppUserRepository : IAppUserRepository
    {
        private ApplicationContext _context;
        
        public void DeleteUser(string id)
        {
            using (_context = new ApplicationContext())
            {
                ApplicationUser user = _context.ApplicationUser.Where(x => x.Id == id.ToString()).FirstOrDefault();
                user.IsDelete = true;
                user.ModifiedDate = DateTime.UtcNow;
                user.ModifiedBy = user.CreatedBy;
                _context.SaveChanges();
            }
        }

        public ApplicationUser GetUser(string id)
        {
            using (_context = new ApplicationContext())
            {
                return _context.ApplicationUser.Where(x => x.Id == id.ToString()).FirstOrDefault();
            }
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            using (_context = new ApplicationContext())
            {
                return _context.ApplicationUser.ToList();
            }
        }

        public void InsertUser(ApplicationUser user)
        {
            using (_context = new ApplicationContext())
            {
                _context.ApplicationUser.Add(user);
                _context.SaveChanges();
            }
        }

        public void UpdateUser(ApplicationUser user)
        {
            using (_context = new ApplicationContext())
            {
                _context.ApplicationUser.Update(user);
                _context.SaveChanges();
            }
        }
    }
}
