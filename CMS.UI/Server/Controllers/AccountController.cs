using CMS.API.Utilities;
using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.UI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly JWTTokenGenerator _jWTTokenGenerator;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private ApplicationContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger, IUserService userService, ApplicationContext context, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _jWTTokenGenerator = new JWTTokenGenerator(config);
            _userService = userService;
            _context = context;
            _config = config;
        }

        public async Task<IActionResult> Register(RegisterFM registerModel)
        {

        }

        public async Task<IActionResult> Login(LoginFM loginUser)
        {
        }

    }
}
