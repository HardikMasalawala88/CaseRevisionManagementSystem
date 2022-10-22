using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using System.Threading.Tasks;

namespace CMS.Services.Interface
{
    public interface IAccountService
    {
        User Register(RegisterFM registerFM);
        void Login(LoginFM loginFM);
    }
}
