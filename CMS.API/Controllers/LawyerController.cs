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
    [Authorize(Roles = ApplicationUserRoles.Lawyer)]
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

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddClient(ClientFM clientFM)
        {
            var loggedInUser = HttpContext.Session.GetString("UserId");
            clientFM.User.CreatedBy = loggedInUser;
            var clientData = _clientService.CreateClient(clientFM);
            if (clientData != null)
            {
                return Ok(new Response { Status = "Success", Message = "Client Added Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Insert Client data failed..!"
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateClient(ClientFM clientForm)
        {
            var loggedInUser = HttpContext.Session.GetString("UserId");
            clientForm.User.ModifiedBy = loggedInUser;
            var clientUpdatedData = _clientService.UpdateClient(clientForm.Id);
            if (clientUpdatedData != null)
            {
                return Ok(new Response { Status = "Success", Message = "Client Data Updated Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Can not update client data..!"
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ClientDetails()
        {
            var clientDetails = _clientService.ListClientData();
            if (clientDetails != null)
            {
                return Ok(clientDetails);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Fetching client data failed..!"
                });
            }

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult RemoveClient(long clientId)
        {
            bool isClientRemoved = _clientService.RemoveClient(clientId);
            if (isClientRemoved)
            {
                return Ok(new Response { Status = "Success", Message = "Client Removed Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Client deletion failed..!"
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddCase(CaseFM caseFM)
        {
            var loggedInUser = HttpContext.Session.GetString("UserId");
            caseFM.Client.CreatedBy = loggedInUser;

            //fetch client and lawyer data
            var clientData = _clientService.GetClientData(caseFM.ClientId);
            caseFM.Client = clientData;
            var lawyerData = _lawyerService.GetLawyerData(caseFM.LawyerId);
            caseFM.Lawyer = lawyerData;

            var caseData = _caseService.CreateOrUpdateCase(caseFM);
            if (caseData != null)
            {
                return Ok(new Response { Status = "Success", Message = "Case Added Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Insert Case data failed..!"
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateCase(CaseFM caseFM)
        {
            var loggedInUser = HttpContext.Session.GetString("UserId");
            caseFM.Client.ModifiedBy = loggedInUser;
            var caseData = _caseService.CreateOrUpdateCase(caseFM);
            if (caseData != null)
            {
                return Ok(new Response { Status = "Success", Message = "Case Updated Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Update Case data failed..!"
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CaseDetails()
        {
            var caseDetails = _caseService.ListCaseDetail();
            if (caseDetails != null)
            {
                return Ok(caseDetails);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Fetching case data failed..!"
                });
            }

        }

        [HttpPost]
        [Route("[action]")]
        // [Authorize(ApplicationUserRoles.Admin)]
        public IActionResult RemoveCase(long caseId)
        {
            bool isCaseRemoved = _caseService.RemoveCaseDetail(caseId);
            if (isCaseRemoved)
            {
                return Ok(new Response { Status = "Success", Message = "Case Removed Successfully...!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Case deletion failed..!"
                });
            }
        }
    }
}
