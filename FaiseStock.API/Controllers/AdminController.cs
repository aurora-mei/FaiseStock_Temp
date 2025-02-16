using AutoMapper;
using FaiseStock.API.Services;
using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using FaiseStock.Data.Models.ViewModels;
using FaiseStock.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FaiseStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
      
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        private readonly IAdminReposity _adminReposity;
        public AdminController(IAdminService adminService, ILogger<AdminController> logger
            ,IAdminReposity adminReposity,IMapper mapper)
        {
            _adminService = adminService;
            _logger = logger;
            _mapper = mapper;
            _adminReposity = adminReposity;
        }

        [HttpPost]
        [Route("/create-contest")]
        //[Authorize]
        public async Task<IActionResult> CreateContest([FromBody] ContestDto contestDto)//create contest
        {
            try
            {
                _logger.LogInformation("CreateContest ne");
               Contest contest =  await _adminService.CreateContestAsync(_mapper.Map<Contest>(contestDto));
              ContestVM finalContestVm = _mapper.Map<ContestVM>(contest);
               return Ok(finalContestVm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("ERROR_AT_CreateContest");
            }
        }
        [HttpGet]
        [Route("/get-contest-participants/{contestId}")]
        //[Authorize]
        public async Task<IActionResult> GetContestParticipants([FromRoute] string contestId)//create contest
        {
            try
            {
                _logger.LogInformation("GetContestParticipants ne");
                List<ContestParticipant> list =  await _adminReposity.GetContestParticipantsAsync(contestId);
                List<ContestParticipantVM> listVm = _mapper.Map< List<ContestParticipantVM>>(list);
                return Ok(listVm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("ERROR_AT_CreateContest");
            }
        }
    }
}
