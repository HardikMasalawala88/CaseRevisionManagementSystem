using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Services.Interface
{
    public interface IUserService
    {
        User GetUserDetails(LoginFM loginUser);
        User GetUserById(long userId);
        IList<string> GetUserRole(User user);
        UserFM CreateOrUpdateUser(UserFM userFM);
    }
}
