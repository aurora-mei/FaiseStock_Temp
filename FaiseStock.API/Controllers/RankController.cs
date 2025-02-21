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
        [Route("generateRank")]
        //[Authorize]
        public async Task<IActionResult> GenerateRank()//will activate at end_date_time
        {
            try
            {
                _logger.LogInformation("Generate rank ne");
                await _rankService.GenerateRankAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("ERROR_AT_GENERATE_RANK");
            }
        }
        [HttpGet]
        [Route("all-rank")]

        //[Authorize]
        public async Task<IActionResult> GetAllRank()
        {
            try
            {
                _logger.LogInformation("Get rank ne");
                var rankDomain = await _rankRepository.GetRankAsync();//truy cập vào trực tiếp bảng regions trong dbcontext thông qua repository
                List<TopUserDto> rankDto = _mapper.Map<List<TopUserDto>>(rankDomain);
                return Ok(rankDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("ERROR_AT_GET_RANK");
            }
        }
        [HttpGet]
        [Route("rankDate/{keydate}")]
        //[Authorize]
        public async Task<IActionResult> GetRankByDate([FromRoute] string keydate)
        {
            try
            {
                _logger.LogInformation("Get rank ne");
                // Validate and parse the date
                if (!DateOnly.TryParse(keydate, out var parsedDate))
                {
                    return BadRequest("Invalid date format. Use 'yyyy-MM-dd'.");
                }
                var rankDomain = await _rankRepository.GetRankByDateAsync(parsedDate);
                List<TopUserDto> rankDto = _mapper.Map<List<TopUserDto>>(rankDomain);
                return Ok(rankDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("ERROR_AT_GET_RANK_{KEYDATE}");
            }
        }
        
        [HttpGet]
        [Route("rankContest/{contestId}")]
        //[Authorize]
        public async Task<IActionResult> GetRankByContest([FromRoute] string contestId)
        {
            try
            {
                _logger.LogInformation("Get rank ne");
                var rankDomain = await _rankRepository.GetRankByContestAsync(contestId);
                List<TopUserDto> rankDto = _mapper.Map<List<TopUserDto>>(rankDomain);
                return Ok(rankDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("ERROR_AT_GET_RANK_{KEYDATE}");
            }
        }
        [HttpDelete]
        [Route("/rank")]
        //[Authorize]
        public IActionResult ClearRank()
        {
            try
            {
                _logger.LogInformation("clear rank ne");
                var success =  _rankRepository.ClearRank();
                return success? Ok("Clear successful"):BadRequest("No hope to clear");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("ERROR_AT_CLEAR_RANK");
            }
        }
    }
}
