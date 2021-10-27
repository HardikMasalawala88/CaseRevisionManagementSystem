using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Interface
{
    public interface IUserService
    {
        //ApplicationUser GetUserDetails(LoginFM loginUser);
        //IList<string> GetUserRole(ApplicationUser user);
        bool CreateOrUpdateUser(ApplicationUserFM userFM);
    }
}
