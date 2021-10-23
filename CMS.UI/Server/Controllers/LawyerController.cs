using CMS.Data.ContextModels;
using CMS.Data.FormModels;
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
    public class LawyerController : Controller
    {
        private readonly ILogger<LawyerController> _logger;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        private readonly ILawyerService _lawyerService;
        private readonly ICaseService _caseService;
        private IConfiguration _config;
        private ApplicationContext _context;
        public LawyerController(ILogger<LawyerController> logger, IClientService clientService, ILawyerService lawyerService, ICaseService caseService, IUserService userService, ApplicationContext context, IConfiguration config)
        {
            _logger = logger;
            _userService = userService;
            _clientService = clientService;
            _lawyerService = lawyerService;
            _caseService = caseService;
            _context = context;
            _config = config;
        }

        public IActionResult AddClient(ClientFM clientFM)
        {
            return View();
        }

        public IActionResult UpdateClient(ClientFM clientFM)
        {
            return View();
        }

        public IActionResult ClientDetails()
        {
            return View();
        }

        public IActionResult RemoveClient(int clientId)
        {
            return View();
        }

        public IActionResult AddCase(CaseFM caseFM)
        {
            return View();
        }

        public IActionResult UpdateCase(CaseFM caseFM)
        {
            return View();
        }

        public IActionResult CaseDetails()
        {
            return View();
        }

        public IActionResult RemoveCase(int caseId)
        {
            return View();
        }
    }
}
