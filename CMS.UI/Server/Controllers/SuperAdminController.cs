using CMS.Data.ContextModels;
using CMS.Services.Interface;
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

        public void AddAdmin()
        {

        }

        public void AddLawyer()
        {

        }

        public void UpdateAdmin()
        {

        }

        public void LawyerDetails()
        {

        }

        public void RemoveLawyer(int lawyerId)
        {

        }
    }
}
