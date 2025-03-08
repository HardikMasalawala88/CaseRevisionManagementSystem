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
        User GetUserDetails(LoginFM loginUser);
        User GetUserById(long userId);
        IList<string> GetUserRole(User user);
        UserFM CreateOrUpdateUser(UserFM userFM);
    }
}
