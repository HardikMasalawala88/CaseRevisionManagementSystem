using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.API.Controllers
{
    [Authorize(ApplicationUserRoles.SuperAdmin)]
    [Authorize(ApplicationUserRoles.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class SuperAdminController : Controller
    {
        private readonly ILogger<SuperAdminController> _logger;
        private readonly IUserService _userService;
        private readonly ILawyerService _lawyerService;
        private IConfiguration _config;
        private ApplicationContext _context;
        public SuperAdminController(ILogger<SuperAdminController> logger, ILawyerService lawyerService, IUserService userService, ApplicationContext context, IConfiguration config)
        {
            _logger = logger;
            _userService = userService;
            _lawyerService = lawyerService;
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddAdmin(UserFM userForm)
        {
            var data = _userService.CreateOrUpdateUser(userForm);

            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddLawyer(LawyerFM lawyerForm)
        {
            //UserFM userForm = new UserFM();
           // var userData = _userService.CreateOrUpdateUser(userForm);
            var lawyerData = _lawyerService.CreateOrUpdateLawyer(lawyerForm);

            return Ok();
        }
    }
}
