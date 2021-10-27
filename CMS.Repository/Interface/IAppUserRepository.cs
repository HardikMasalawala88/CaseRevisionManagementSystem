using CMS.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository.Interface
{
    public interface IAppUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers();
        ApplicationUser GetUser(string id);
        void InsertUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(string id);
    }
}
