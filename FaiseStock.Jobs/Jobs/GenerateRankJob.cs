using FaiseStock.API.Services;
using FaiseStock.Data;
using FaiseStock.Data.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FaiseStock.Jobs.Jobs
{
    public class GenerateRankJob : IJob
    {
        private readonly IRankService _rankService;
        private readonly ILogger<GenerateRankJob> _logger;
        private readonly ISchedulerFactory _schedulerFactory;

        public GenerateRankJob(IRankService rankService, ISchedulerFactory schedulerFactory, ILogger<GenerateRankJob> logger)
        {
            _rankService = rankService;
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Generate Rank Job is running");
                await _rankService.GenerateRankAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("GENERATE RANK JOB EXCEPTION: " + ex.Message);
                throw;
            }
            finally
            {
                var scheduler = await _schedulerFactory.GetScheduler();
                await scheduler.DeleteJob(context.JobDetail.Key);
                _logger.LogInformation("Deleted GenerateRankJob after execution.");
            }
        }
    }
}
