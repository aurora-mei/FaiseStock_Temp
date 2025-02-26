using AutoMapper;
using FaiseStock.API.Services;
using FaiseStock.Data.Models.Dtos;
using FaiseStock.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FaiseStock.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RankController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RankController> _logger;
        private readonly IRankRepository _rankRepository;
        private readonly IRankService _rankService;
        public RankController(IRankRepository rankRepository, ILogger<RankController> logger, IMapper mapper, IRankService rankService)
        {
            _rankRepository = rankRepository;
            _logger = logger;
            _mapper = mapper;
            _rankService = rankService;
        }
        [HttpGet]
        [Route("all-rank")]
        public async Task<IActionResult> GetAllRank()
        {
                _logger.LogInformation("Do GetAllRank");
                var rankDomain = await _rankRepository.GetRankAsync();
                List<TopUserDto> rankDto = _mapper.Map<List<TopUserDto>>(rankDomain);
                return Ok(rankDto);
        }
        [HttpGet]
        [Route("rankContest/{contestId}")]
        public async Task<IActionResult> GetRankByContest([FromRoute] string contestId)
        {
                _logger.LogInformation("Do GetRankByContest");
                var rankDomain = await _rankRepository.GetRankByContestAsync(contestId);
                List<TopUserDto> rankDto = _mapper.Map<List<TopUserDto>>(rankDomain);
                return Ok(rankDto);
        }

    }
}
