using CMS.API.Utilities;
using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterFM registerModel)
        {
            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User Already Exists..!"
                });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // Create User
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User Creation Failed..!"
                });
            }

            //Checking roles in db and creating if not exists
            //if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.SuperAdmin))
            //{
            //    await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.SuperAdmin));
            //}
            if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.User));
            }
            if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.Lawyer))
            {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Lawyer));
            }

            //Add role to user
            if (!string.IsNullOrEmpty(registerModel.Role) && registerModel.Role == ApplicationUserRoles.Admin)
            {
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.Admin);
            }
            //else if (!string.IsNullOrEmpty(registerModel.Role) && registerModel.Role == ApplicationUserRoles.SuperAdmin)
            //{
            //    await _userManager.AddToRoleAsync(user, ApplicationUserRoles.SuperAdmin);
            //}
            else if (!string.IsNullOrEmpty(registerModel.Role) && registerModel.Role == ApplicationUserRoles.Lawyer)
            {
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.Lawyer);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.User);
            }

            return Ok(new Response { Status = "Success", Message = "User Created Successfully...!",Data = user });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginFM loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.SuperAdmin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.SuperAdmin));
                }
                if (!string.IsNullOrEmpty(user.Role) && user.Role == ApplicationUserRoles.SuperAdmin)
                {
                    await _userManager.AddToRoleAsync(user, ApplicationUserRoles.SuperAdmin);
                }
                IList<string> userRole = await _userManager.GetRolesAsync(user);
                string token = _jWTTokenGenerator.GenerateLoginToken(user.UserName, userRole);
                HttpContext.Session.SetString("UserId", user.Id);
                return Ok(new Response { Status = "Success", Message = "User LoggedIn Successfully...!", Data = token });
            }
            //var userData = _userService.GetUserDetails(loginUser);
            //if (userData != null)
            //{
            //    HttpContext.Session.SetString("UserId", userData.Id.ToString());
            //    IList<string> userRole = _userService.GetUserRole(userData);
            //    string token = _jWTTokenGenerator.GenerateLoginToken(userData.Username, userRole);
            //    return Ok(new Response { Status = "Success", Message = "User LoggedIn Successfully...!", Data = token });
            //}
            return Unauthorized();
        }
    }
}
