using AutoMapper;
using FaiseStock.Data.Models.Dtos;
using FaiseStock.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FaiseStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
    
        [HttpGet]
        [Route("/generateRank")]
        //[Authorize]
        public async Task<IActionResult> GenerateRank()
        {
            try
            {
                _logger.LogInformation("Generate rank ne");
                await _userRepository.GenerateRankAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("ERROR_AT_GENERATE_RANK");
            }
        }
        [HttpGet]
        [Route("/rank")]

        //[Authorize]
        public async Task<IActionResult> GetRank()
        {
            try
            {
                _logger.LogInformation("Get rank ne");
                var rankDomain = await _userRepository.GetRankAsync();//truy cập vào trực tiếp bảng regions trong dbcontext thông qua repository
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
        [Route("/rank/{keydate}")]
        //[Authorize]
        public async Task<IActionResult> GetRank([FromRoute] string keydate)
        {
            try
            {
                _logger.LogInformation("Get rank ne");
                // Validate and parse the date
                if (!DateOnly.TryParse(keydate, out var parsedDate))
                {
                    return BadRequest("Invalid date format. Use 'yyyy-MM-dd'.");
                }
                var rankDomain = await _userRepository.GetRankAsync(parsedDate);
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
                var success =  _userRepository.ClearRank();
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
