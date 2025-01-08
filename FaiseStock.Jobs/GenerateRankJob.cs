using FaiseStock.Data.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FaiseStock.API.Services.Schedule
{
    public class GenerateRankJob:IJob
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GenerateRankJob> _logger;
        public GenerateRankJob(IUserRepository userRepository, ILogger<GenerateRankJob> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Generate Rank Job is running");
                await _userRepository.GenerateRankAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GENERATE RANK JOB EXCEPTION: " +ex.Message);
                throw;
            }
        }
    }
}
