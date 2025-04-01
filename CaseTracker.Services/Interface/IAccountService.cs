using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using System.Threading.Tasks;

namespace CaseTracker.Services.Interface
{
    public interface IAccountService
    {
        User Register(RegisterFM registerFM);
        User Login(LoginFM loginFM);
    }
}
