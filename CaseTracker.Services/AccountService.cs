using AutoMapper;
using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Repository.Interface;
using CaseTracker.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CaseTracker.Services
{
    public class AccountService : IAccountService
    {
        private NavigationManager _navigationManager;
        private readonly ApplicationContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ApplicationUser User { get; private set; }

        public AccountService(NavigationManager navigationManager, ApplicationContext context,IMapper mapper, IUserRepository userRepository)
        {
            _navigationManager = navigationManager;
            _userRepository = userRepository;
            _context = context;
            _mapper = mapper;
        }

        async Task<ApplicationUser> IAccountService.RegisterLawyerAsync(RegisterFM registerFM)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Step 1: Insert user into Identity
                var user = _mapper.Map<ApplicationUser>(registerFM);
                user.CreatedBy = "CaseTracker";

                var identityResult = await _userRepository.InsertUserAsync(user, registerFM.Password);
                if (!identityResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return user;
                }

                // Step 2: Insert Lawyer linked to Identity User
                var lawyer = new Lawyer
                {
                    UserId = user.Id,  // FK reference to AspNetUsers
                    LawyerUniqueNumber = registerFM.LawyerFM.LawyerUniqueNumber,
                    Specialization = registerFM.Specialization.ToString(),
                    CreatedBy = user.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };

                await _context.Lawyers.AddAsync(lawyer);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return user;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        async Task<ApplicationUser> IAccountService.RegisterAsync(RegisterFM registerFM)
        {
            try
            {
                ApplicationUser user = new ApplicationUser();
                if (registerFM != null)
                {
                    registerFM.Role = "Lawyer";
                    user = _mapper.Map<ApplicationUser>(registerFM);
                    user.CreatedBy = "CaseTracker";
                }

                var identityResult = await _userRepository.InsertUserAsync(user, registerFM.Password);
                if (identityResult.Succeeded)
                { 
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task<ApplicationUser> IAccountService.LoginAsync(LoginFM loginFM)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginFM.Username))
                {
                    ApplicationUser userData = await _userRepository.GetLoggedInUserAsync(loginFM.Username);

                    //if(userData is not null && (userData.Role == ApplicationUserRoles.User || userData.Role == ApplicationUserRoles.Lawyer))
                    //{
                    //    _navigationManager.NavigateTo("user/dashboard");
                    //}

                    return userData;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }
    }
}
