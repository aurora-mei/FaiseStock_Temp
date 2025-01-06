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
        //[Authorize]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                _logger.LogInformation("HELLO NE");
                var regions = await _userRepository.GetAllAsync();//truy cập vào trực tiếp bảng regions trong dbcontext thông qua repository
                return Ok(regions);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetTotalDeposits([FromRoute] string id)
        {
            try
            {
                _logger.LogInformation("Get total ne");
                var regions = await _userRepository.CalculateTotalDeposit(id);//truy cập vào trực tiếp bảng regions trong dbcontext thông qua repository
                return Ok("lslls: "+regions);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        [HttpGet("/generateRank")]
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
                throw;
            }
        }
        [HttpGet("/rank")]
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
                throw;
            }
        }
        [HttpGet("/rank/{keydate}")]
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
                var rankDomain = await _userRepository.GetRankAsync(parsedDate);//truy cập vào trực tiếp bảng regions trong dbcontext thông qua repository
                List<TopUserDto> rankDto = _mapper.Map<List<TopUserDto>>(rankDomain);
                return Ok(rankDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        //[HttpGet("/rank")]
        ////[Authorize]
        //public async Task<IActionResult> GetRank()
        //{
        //    try
        //    {
        //        _logger.LogInformation("HELLO NE 2");
        //        var regions = await _userRepository.GetRankAsync();//truy cập vào trực tiếp bảng regions trong dbcontext thông qua repository
        //        return Ok(regions);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        throw;
        //    }


        //}
    }
}
