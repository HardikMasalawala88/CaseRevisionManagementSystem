using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var Admindata = _userService.CreateOrUpdateUser(userForm);
            if(Admindata != null)
            {
                return Ok(new Response { Status = "Success", Message = "Admin Added Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Insert Admin data failed..!"
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
      //  [Authorize(ApplicationUserRoles.Admin)]
        public IActionResult AddLawyer(LawyerFM lawyerForm)
        {
            var loggedInUser = HttpContext.Session.GetString("UserId");
            lawyerForm.User.CreatedBy = loggedInUser;
            var lawyerData = _lawyerService.CreateOrUpdateLawyer(lawyerForm);
            if(lawyerData != null)
            {
                return Ok(new Response { Status = "Success", Message = "Lawyer Added Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Insert lawyer data failed..!"
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        //[Authorize(ApplicationUserRoles.Admin)]
        public IActionResult UpdateLawyer(LawyerFM lawyerForm)
        {
            var loggedInUser = HttpContext.Session.GetString("UserId");
            lawyerForm.User.ModifiedBy = loggedInUser;
            var lawyerUpdatedData = _lawyerService.CreateOrUpdateLawyer(lawyerForm);
            if(lawyerUpdatedData != null)
            {
                return Ok(new Response { Status = "Success", Message = "Lawyer Data Updated Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Can not update lawyer data..!"
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
       // [Authorize(ApplicationUserRoles.Admin)]
        public IActionResult LawyerDetails()
        {
            var lawyerDetails = _lawyerService.ListLawyerData().FirstOrDefault();
            if(lawyerDetails != null)
            {
                return Ok(lawyerDetails);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Fetching lawyer data failed..!"
                });
            }
            
        }

        [HttpPost]
        [Route("[action]")]
       // [Authorize(ApplicationUserRoles.Admin)]
        public IActionResult RemoveLawyer(long lawyerId)
        {
            bool isLawyerRemoved = _lawyerService.RemoveLawyerData(lawyerId);
            if (isLawyerRemoved)
            {
                return Ok(new Response { Status = "Success", Message = "Lawyer Removed Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Lawyer deletion failed..!"
                });
            }
        }
    }
}
