﻿using AutoMapper;
using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using FaiseStock.Data.Models.ViewModels;
using FaiseStock.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FaiseStock.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        public UserController(IUserRepository userRepository, IMapper mapper, ILogger<AdminController> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
     
        [HttpGet]
        [Route("get-all-contest")]
        public async Task<IActionResult> GetAllContest()//create contest
        {
                _logger.LogInformation("Do GetAllContest");
                List<Contest> list = await _userRepository.GetAllContestAsync();
                List<ContestVM> listVm = _mapper.Map<List<ContestVM>>(list);
                return Ok(listVm);
        }
        [HttpGet]
        [Route("get-contest/{id}")]
        public async Task<IActionResult> GetContestById(string id)//create contest
        {
                _logger.LogInformation("Do GetContestById");
                Contest contest = await _userRepository.GetContestByIdAsync(id);
                ContestVM contestVm = _mapper.Map<ContestVM>(contest);
                return Ok(contestVm);
        }
        [HttpPost]
        [Route("register-contest")]
        public async Task<ActionResult> RegisterContest([FromBody] ContestParticipantDto contestParticipantDto)
        {
            _logger.LogInformation("Do RegisterContest");
            ContestParticipant contestParticipant = await _userRepository.AddContestParticipant(_mapper.Map<ContestParticipant>(contestParticipantDto));
            return Ok(_mapper.Map<ContestParticipantVM>(contestParticipant));
        }
        [HttpGet]
        [Route("get-contest-participants/{contestId}")]
        public async Task<IActionResult> GetContestParticipants([FromRoute] string contestId)//create contest
        {
            
                _logger.LogInformation("Do GetContestParticipants");
                List<ContestParticipant> list = await _userRepository.GetContestParticipantsAsync(contestId);
                List<ContestParticipantVM> listVm = _mapper.Map<List<ContestParticipantVM>>(list);
                return Ok(listVm);
          
        }
    }
}
