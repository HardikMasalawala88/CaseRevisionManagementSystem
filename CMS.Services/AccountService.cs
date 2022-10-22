using AutoMapper;
using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Repository;
using CMS.Repository.Interface;
using CMS.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class AccountService : IAccountService
    {
        //private IHttpService _httpService;
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


        public void Login(LoginFM loginFM)
        {
            try
            {
                if(!string.IsNullOrEmpty(loginFM.Username))
                {
                    User userData = _userRepository.GetLoggedInUser(loginFM.Username);

                    if(userData is not null)
                    {
                        _navigationManager.NavigateTo("case/caselist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User Register(RegisterFM registerFM)
        {
            try
            {
                User user = new User();
                if (registerFM != null)
                {
                    registerFM.Role = "User";
                    user = _mapper.Map<User>(registerFM);
                    user.CreatedBy = "CMS";
                }
                var userData = _userRepository.InsertUser(user);
                return userData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
