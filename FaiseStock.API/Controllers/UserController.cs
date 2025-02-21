using AutoMapper;
using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using FaiseStock.Data.Models.ViewModels;
using FaiseStock.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FaiseStock.API.Controllers
{
    [Route("[controller]")]
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
        [HttpPost]
        [Route("update-balance")]
        public async Task<ActionResult> UpdateBalance([FromBody] WalletDto walletDto)
        {
            _logger.LogInformation("CreateDeposit ne");
            Wallet walletDomain = await _userRepository.UpdateBalanceAsync(_mapper.Map<Wallet>(walletDto));
            return Ok(_mapper.Map<WalletVM>(walletDomain));
        }
        [HttpPost]
        [Route("register-contest")]
        public async Task<ActionResult> RegisterContest([FromBody] ContestParticipantDto contestParticipantDto)
        {
            _logger.LogInformation("RegisterContest ne");
            ContestParticipant contestParticipant = await _userRepository.AddContestParticipant(_mapper.Map<ContestParticipant>(contestParticipantDto));
            return Ok(_mapper.Map<ContestParticipantVM>(contestParticipant));
        }
    }
}
