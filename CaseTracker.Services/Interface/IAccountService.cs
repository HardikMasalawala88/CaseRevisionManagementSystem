using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CaseTracker.Services.Interface
{
    public interface IAccountService
    {
        Task<ApplicationUser> RegisterLawyerAsync(RegisterFM registerFM);
        Task<ApplicationUser> RegisterAsync(RegisterFM registerFM);
        Task<ApplicationUser> LoginAsync(LoginFM loginFM);
    }
}
