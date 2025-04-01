using AutoMapper;
using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Repository.Interface;
using CaseTracker.Services.Interface;
using Microsoft.AspNetCore.Components;
using System;

namespace CaseTracker.Services
{
    public class AccountService : IAccountService
    {
        private NavigationManager _navigationManager;
        private readonly ApplicationContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public User User { get; private set; }

        public AccountService(NavigationManager navigationManager, ApplicationContext context,IMapper mapper, IUserRepository userRepository)
        {
            _navigationManager = navigationManager;
            _userRepository = userRepository;
            _context = context;
            _mapper = mapper;
        }


        public User Login(LoginFM loginFM)
        {
            try
            {
                if(!string.IsNullOrEmpty(loginFM.Username))
                {
                    User userData = _userRepository.GetLoggedInUser(loginFM.Username);

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

        public User Register(RegisterFM registerFM)
        {
            try
            {
                User user = new User();
                if (registerFM != null)
                {
                    registerFM.Role = "Lawyer";
                    user = _mapper.Map<User>(registerFM);
                    user.CreatedBy = "CaseTracker";
                }

                var userData = _userRepository.InsertUser(user);
                return userData;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
