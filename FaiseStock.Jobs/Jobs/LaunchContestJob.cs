using FaiseStock.API.Services;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FaiseStock.Jobs.Jobs;

    public class LaunchContestJob : IJob
    {
        private readonly IRankService _rankService;
        private readonly ILogger<LaunchContestJob> _logger;
        private readonly ISchedulerFactory _schedulerFactory;

        public LaunchContestJob(IRankService rankService, ISchedulerFactory schedulerFactory, ILogger<LaunchContestJob> logger)
        {
            _rankService = rankService;
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Launch Contest Job is running");
                await _rankService.LaunchContestAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("LAUNCH CONTEST JOB EXCEPTION: " + ex.Message);
                throw;
            }
            finally
            {
                var scheduler = await _schedulerFactory.GetScheduler();
                await scheduler.DeleteJob(context.JobDetail.Key);
                _logger.LogInformation("Deleted LaunchContestJob after execution.");
            }
        }
    }
